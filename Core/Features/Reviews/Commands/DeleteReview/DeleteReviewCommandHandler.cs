namespace Core.Features.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteReviewCommand, ApiResponse<string>>
{
    private readonly IReviewService _reviewService;
    private readonly ICurrentUserService _currentUserService;

    public DeleteReviewCommandHandler(
        IReviewService reviewService,
        ICurrentUserService currentUserService) : base()
    {
        _reviewService = reviewService;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var currentCustomerId = _currentUserService.GetUserId();
        var review = await _reviewService.GetReviewByIdsAsync(request.ProductId, currentCustomerId);
        if (review == null) return NotFound<string>(SharedResourcesKeys.ReviewNotFound);
        var result = await _reviewService.DeleteReviewAsync(review);
        if (result == "Success") return Deleted<string>();
        return BadRequest<string>(SharedResourcesKeys.DeleteFailed);
    }
}

