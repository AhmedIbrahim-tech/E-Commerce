using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Commands.UpdateUserRoles;

public class UpdateUserRolesCommandHandler : ApiResponseHandler,
    IRequestHandler<UpdateUserRolesCommand, ApiResponse<string>>
{
    private readonly IAuthorizationService _authorizationService;

    public UpdateUserRolesCommandHandler(IAuthorizationService authorizationService) : base()
    {
        _authorizationService = authorizationService;
    }

    public async Task<ApiResponse<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.UpdateUserRoles(request);
        switch (result)
        {
            case "UserIsNull": return NotFound<string>();
            case "FailedToRemoveOldRoles": return BadRequest<string>(SharedResourcesKeys.FailedToRemoveOldRoles);
            case "FailedToAddNewRoles": return BadRequest<string>(SharedResourcesKeys.FailedToAddNewRoles);
            case "FailedToUpdateUserRoles": return BadRequest<string>(SharedResourcesKeys.FailedToUpdateUserRoles);
        }
        return Edit("");
    }
}

