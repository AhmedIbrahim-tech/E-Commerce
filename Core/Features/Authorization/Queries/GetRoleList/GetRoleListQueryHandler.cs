namespace Core.Features.Authorization.Queries.GetRoleList;

public class GetRoleListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetRoleListQuery, ApiResponse<List<GetRoleListResponse>>>
{
    private readonly RoleManager<Role> _roleManager;

    public GetRoleListQueryHandler(RoleManager<Role> roleManager) : base()
    {
        _roleManager = roleManager;
    }

    public async Task<ApiResponse<List<GetRoleListResponse>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Role, GetRoleListResponse>> expression = role => new GetRoleListResponse
        {
            RoleId = role.Id,
            RoleName = role.Name
        };

        var roleList = await _roleManager.Roles
            .Select(expression)
            .ToListAsync(cancellationToken);

        if (roleList is null || !roleList.Any()) return NotFound<List<GetRoleListResponse>>(SharedResourcesKeys.NotFound);
        return Success(roleList);
    }
}

