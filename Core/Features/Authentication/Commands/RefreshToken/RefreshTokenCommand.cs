namespace Core.Features.Authentication.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<ApiResponse<JwtAuthResponse>>;

