namespace Core.Features.Authentication.GoogleLogin;

public record GoogleLoginCommand(string IdToken) : IRequest<ApiResponse<JwtAuthResponse>>;

