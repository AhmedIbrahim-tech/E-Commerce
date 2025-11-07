namespace Core.Features.Reviews.Commands.EditReview;

public record EditReviewCommand(Guid ProductId, Rating Rating, string? Comment) : IRequest<ApiResponse<string>>;

