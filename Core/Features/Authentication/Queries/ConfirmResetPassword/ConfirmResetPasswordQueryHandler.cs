using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authentication.ConfirmResetPassword;

public class ConfirmResetPasswordQueryHandler : ApiResponseHandler,
    IRequestHandler<ConfirmResetPasswordQuery, ApiResponse<string>>
{
    private readonly IAuthenticationService _authenticationService;

    public ConfirmResetPasswordQueryHandler(IAuthenticationService authenticationService) : base()
    {
        _authenticationService = authenticationService;
    }

    public async Task<ApiResponse<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
    {
        var confirmResetPasswordResult = await _authenticationService.ConfirmResetPasswordAsync(request.Code, request.Email);
        return confirmResetPasswordResult switch
        {
            "UserNotFound" => BadRequest<string>(SharedResourcesKeys.UserNotFound),
            "Success" => Success(""),
            _ => BadRequest<string>(SharedResourcesKeys.InvaildCode)
        };
    }
}

