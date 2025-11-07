using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authentication.SendResetPassword;

public class SendResetPasswordCommandHandler : ApiResponseHandler,
    IRequestHandler<SendResetPasswordCommand, ApiResponse<string>>
{
    private readonly IAuthenticationService _authenticationService;

    public SendResetPasswordCommandHandler(IAuthenticationService authenticationService) : base()
    {
        _authenticationService = authenticationService;
    }

    public async Task<ApiResponse<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var resetPasswordResult = await _authenticationService.SendResetPasswordCodeAsync(request.Email);
        return resetPasswordResult switch
        {
            "Success" => Success(""),
            "UserNotFound" => BadRequest<string>(SharedResourcesKeys.UserNotFound),
            _ => BadRequest<string>(SharedResourcesKeys.TryAgainLater)
        };
    }
}

