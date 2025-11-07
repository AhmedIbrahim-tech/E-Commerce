using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authentication.ResetPassword;

public class ResetPasswordCommandHandler : ApiResponseHandler,
    IRequestHandler<ResetPasswordCommand, ApiResponse<string>>
{
    private readonly IAuthenticationService _authenticationService;

    public ResetPasswordCommandHandler(IAuthenticationService authenticationService) : base()
    {
        _authenticationService = authenticationService;
    }

    public async Task<ApiResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var resetPasswordResult = await _authenticationService.ResetPasswordAsync(request.Email, request.NewPassword);
        return resetPasswordResult switch
        {
            "Success" => Success(""),
            "UserNotFound" => BadRequest<string>(SharedResourcesKeys.UserNotFound),
            _ => BadRequest<string>(SharedResourcesKeys.InvaildCode)
        };
    }
}

