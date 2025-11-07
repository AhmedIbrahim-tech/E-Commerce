using Domain.Requests;

namespace Core.Features.Authorization.Commands.UpdateUserRoles;

public class UpdateUserRolesCommand : UpdateUserRolesRequest, IRequest<ApiResponse<string>>
{
}

