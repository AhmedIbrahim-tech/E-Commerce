using Domain.Responses;

namespace Core.Features.Authorization.Queries.ManageUserRoles;

public record ManageUserRolesQuery(Guid UserId) : IRequest<ApiResponse<ManageUserRolesResponse>>;

