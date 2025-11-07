namespace Core.Features.Authentication.ConfirmResetPassword;

public record ConfirmResetPasswordQuery(string Code, string Email) : IRequest<ApiResponse<string>>;

