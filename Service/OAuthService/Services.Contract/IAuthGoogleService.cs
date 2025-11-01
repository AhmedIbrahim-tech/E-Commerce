namespace Service.OAuthService.Services.Contract;

public interface IAuthGoogleService
{
    Task<(JwtAuthResponse, string)> AuthenticateWithGoogleAsync(string idToken);
    Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken);
}