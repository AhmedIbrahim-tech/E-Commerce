using Domain.Responses;
using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Queries.ManageUserRoles;

public class ManageUserRolesQueryHandler : ApiResponseHandler,
    IRequestHandler<ManageUserRolesQuery, ApiResponse<ManageUserRolesResponse>>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly UserManager<User> _userManager;

    public ManageUserRolesQueryHandler(IAuthorizationService authorizationService, UserManager<User> userManager) : base()
    {
        _authorizationService = authorizationService;
        _userManager = userManager;
    }

    public async Task<ApiResponse<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) return NotFound<ManageUserRolesResponse>(SharedResourcesKeys.NotFound);
        var result = await _authorizationService.ManageUserRolesData(user);
        return Success(result);
    }
}

