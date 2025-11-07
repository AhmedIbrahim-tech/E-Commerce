using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Queries.GetRoleById;

public class GetRoleByIdQueryHandler : ApiResponseHandler,
    IRequestHandler<GetRoleByIdQuery, ApiResponse<GetRoleByIdResponse>>
{
    private readonly RoleManager<Role> _roleManager;

    public GetRoleByIdQueryHandler(RoleManager<Role> roleManager) : base()
    {
        _roleManager = roleManager;
    }

    public async Task<ApiResponse<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Role, GetRoleByIdResponse>> expression = role => new GetRoleByIdResponse
        {
            RoleId = role.Id,
            RoleName = role.Name!
        };

        var role = await _roleManager.Roles
            .Where(r => r.Id == request.Id)
            .Select(expression)
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null) return NotFound<GetRoleByIdResponse>(SharedResourcesKeys.NotFound);
        return Success(role);
    }
}

