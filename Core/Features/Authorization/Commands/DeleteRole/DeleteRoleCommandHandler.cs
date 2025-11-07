using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Commands.DeleteRole;

public class DeleteRoleCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteRoleCommand, ApiResponse<string>>
{
    private readonly IAuthorizationService _authorizationService;

    public DeleteRoleCommandHandler(IAuthorizationService authorizationService) : base()
    {
        _authorizationService = authorizationService;
    }

    public async Task<ApiResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.DeleteRoleAsync(request.RoleId);
        if (result == "NotFound") return NotFound<string>();
        else if (result == "Success") return Deleted<string>();
        else if (result == "Used") return BadRequest<string>(SharedResourcesKeys.RoleIsUsed);
        return BadRequest<string>(result);
    }
}

