using Domain.Responses;
using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Queries.ManageUserClaims;

public class ManageUserClaimsQueryHandler : ApiResponseHandler,
    IRequestHandler<ManageUserClaimsQuery, ApiResponse<ManageUserClaimsResponse>>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly UserManager<User> _userManager;

    public ManageUserClaimsQueryHandler(IAuthorizationService authorizationService, UserManager<User> userManager) : base()
    {
        _authorizationService = authorizationService;
        _userManager = userManager;
    }

    public async Task<ApiResponse<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) return NotFound<ManageUserClaimsResponse>(SharedResourcesKeys.NotFound);
        var result = await _authorizationService.ManageUserClaimsData(user);
        return Success(result);
    }
}

