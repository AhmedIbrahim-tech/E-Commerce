namespace Core.Features.Reviews.Commands.AddReview;

public record AddReviewCommand(Guid ProductId, Rating Rating, string? Comment) : IRequest<ApiResponse<string>>;

