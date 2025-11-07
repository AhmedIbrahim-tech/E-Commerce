using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Commands.EditRole;

public class EditRoleCommandHandler : ApiResponseHandler,
    IRequestHandler<EditRoleCommand, ApiResponse<string>>
{
    private readonly IAuthorizationService _authorizationService;

    public EditRoleCommandHandler(IAuthorizationService authorizationService) : base()
    {
        _authorizationService = authorizationService;
    }

    public async Task<ApiResponse<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.EditRoleAsync(request.RoleName, request.RoleId);
        if (result == "NotFound") return NotFound<string>();
        else if (result == "Success") return Edit("");
        return BadRequest<string>(SharedResourcesKeys.UpdateFailed);
    }
}

