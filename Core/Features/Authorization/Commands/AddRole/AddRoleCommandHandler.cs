using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Commands.AddRole;

public class AddRoleCommandHandler : ApiResponseHandler,
    IRequestHandler<AddRoleCommand, ApiResponse<string>>
{
    private readonly IAuthorizationService _authorizationService;

    public AddRoleCommandHandler(IAuthorizationService authorizationService) : base()
    {
        _authorizationService = authorizationService;
    }

    public async Task<ApiResponse<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.AddRoleAsync(request.RoleName);
        if (result == "Success") return Created("");
        return BadRequest<string>(SharedResourcesKeys.CreateFailed);
    }
}

