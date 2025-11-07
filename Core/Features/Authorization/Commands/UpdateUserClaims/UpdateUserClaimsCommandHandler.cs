using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Commands.UpdateUserClaims;

public class UpdateUserClaimsCommandHandler : ApiResponseHandler,
    IRequestHandler<UpdateUserClaimsCommand, ApiResponse<string>>
{
    private readonly IAuthorizationService _authorizationService;

    public UpdateUserClaimsCommandHandler(IAuthorizationService authorizationService) : base()
    {
        _authorizationService = authorizationService;
    }

    public async Task<ApiResponse<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.UpdateUserClaims(request);
        switch (result)
        {
            case "UserIsNull": return NotFound<string>();
            case "FailedToRemoveOldClaims": return BadRequest<string>(SharedResourcesKeys.FailedToRemoveOldClaims);
            case "FailedToAddNewClaims": return BadRequest<string>(SharedResourcesKeys.FailedToAddNewClaims);
            case "FailedToUpdateUserClaims": return BadRequest<string>(SharedResourcesKeys.FailedToUpdateUserClaims);
        }
        return Edit("");
    }
}

