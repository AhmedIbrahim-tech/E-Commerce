namespace Core.Features.Authentication.SendResetPassword;

public record SendResetPasswordCommand(string Email) : IRequest<ApiResponse<string>>;

