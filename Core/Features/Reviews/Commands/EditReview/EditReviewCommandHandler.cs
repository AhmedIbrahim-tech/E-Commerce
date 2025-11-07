namespace Core.Features.Reviews.Commands.EditReview;

public class EditReviewCommandHandler : ApiResponseHandler,
    IRequestHandler<EditReviewCommand, ApiResponse<string>>
{
    private readonly IReviewService _reviewService;
    private readonly ICurrentUserService _currentUserService;

    public EditReviewCommandHandler(
        IReviewService reviewService,
        ICurrentUserService currentUserService) : base()
    {
        _reviewService = reviewService;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> Handle(EditReviewCommand request, CancellationToken cancellationToken)
    {
        var currentCustomerId = _currentUserService.GetUserId();
        var review = await _reviewService.GetReviewByIdsAsync(request.ProductId, currentCustomerId);
        if (review == null) return NotFound<string>(SharedResourcesKeys.ReviewNotFound);

        review.Rating = request.Rating;
        review.Comment = request.Comment;

        var result = await _reviewService.EditReviewAsync(review);
        if (result == "Success") return Edit("");
        return BadRequest<string>(SharedResourcesKeys.UpdateFailed);
    }
}

