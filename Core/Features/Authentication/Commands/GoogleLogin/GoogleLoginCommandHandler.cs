using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authentication.GoogleLogin;

public class GoogleLoginCommandHandler : ApiResponseHandler,
    IRequestHandler<GoogleLoginCommand, ApiResponse<JwtAuthResponse>>
{
    private readonly IAuthGoogleService _authGoogleService;

    public GoogleLoginCommandHandler(IAuthGoogleService authGoogleService) : base()
    {
        _authGoogleService = authGoogleService;
    }

    public async Task<ApiResponse<JwtAuthResponse>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var (response, message) = await _authGoogleService.AuthenticateWithGoogleAsync(request.IdToken);
        return message switch
        {
            "Success" => Success(response),
            "InvalidGoogleToken" => BadRequest<JwtAuthResponse>(SharedResourcesKeys.InvalidGoogleToken),
            "FailedToAddNewRoles" => BadRequest<JwtAuthResponse>(SharedResourcesKeys.FailedToAddNewRoles),
            "FailedToAddNewClaims" => BadRequest<JwtAuthResponse>(SharedResourcesKeys.FailedToAddNewClaims),
            "GoogleAuthenticationFailed" => BadRequest<JwtAuthResponse>(SharedResourcesKeys.GoogleAuthenticationFailed),
            _ => BadRequest<JwtAuthResponse>(message),
        };
    }
}

