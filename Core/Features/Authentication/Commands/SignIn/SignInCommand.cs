namespace Core.Features.Authentication.SignIn;

public record SignInCommand(string UserName, string Password) : IRequest<ApiResponse<JwtAuthResponse>>;

