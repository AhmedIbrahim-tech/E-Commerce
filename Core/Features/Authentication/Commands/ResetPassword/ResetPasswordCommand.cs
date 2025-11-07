namespace Core.Features.Authentication.ResetPassword;

public record ResetPasswordCommand
    (string Email,
    string NewPassword,
    string ConfirmPassword) : IRequest<ApiResponse<string>>;

