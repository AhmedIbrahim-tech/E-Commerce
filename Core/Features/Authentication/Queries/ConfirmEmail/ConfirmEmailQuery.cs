namespace Core.Features.Authentication.ConfirmEmail;

public record ConfirmEmailQuery(Guid UserId, string Code) : IRequest<ApiResponse<string>>;

