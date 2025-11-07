namespace Core.Features.Reviews.Commands.DeleteReview;

public record DeleteReviewCommand(Guid ProductId) : IRequest<ApiResponse<string>>;

