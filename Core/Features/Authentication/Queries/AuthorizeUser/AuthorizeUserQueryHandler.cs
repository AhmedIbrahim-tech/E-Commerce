using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authentication.AuthorizeUser;

public class AuthorizeUserQueryHandler : ApiResponseHandler,
    IRequestHandler<AuthorizeUserQuery, ApiResponse<string>>
{
    private readonly IAuthenticationService _authenticationService;

    public AuthorizeUserQueryHandler(IAuthenticationService authenticationService) : base()
    {
        _authenticationService = authenticationService;
    }

    public async Task<ApiResponse<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.ValidateToken(request.AccessToken);
        if (result == "NotExpired")
            return Success(result);
        return Unauthorized<string>(SharedResourcesKeys.TokenIsExpired);
    }
}

