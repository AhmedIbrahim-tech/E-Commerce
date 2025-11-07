namespace Core.Features.Reviews.Commands.AddReview;

public class AddReviewCommandHandler : ApiResponseHandler,
    IRequestHandler<AddReviewCommand, ApiResponse<string>>
{
    private readonly IReviewService _reviewService;
    private readonly ICurrentUserService _currentUserService;

    public AddReviewCommandHandler(
        IReviewService reviewService,
        ICurrentUserService currentUserService) : base()
    {
        _reviewService = reviewService;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            ProductId = request.ProductId,
            Rating = request.Rating,
            Comment = request.Comment,
            CustomerId = _currentUserService.GetUserId(),
            CreatedAt = DateTimeOffset.UtcNow.ToLocalTime()
        };

        var result = await _reviewService.AddReviewAsync(review);
        if (result == "Success") return Created("");
        return BadRequest<string>(SharedResourcesKeys.CreateFailed);
    }
}

