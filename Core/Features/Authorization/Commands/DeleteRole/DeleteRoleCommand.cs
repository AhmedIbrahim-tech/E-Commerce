namespace Core.Features.Authorization.Commands.DeleteRole;

public record DeleteRoleCommand(Guid RoleId) : IRequest<ApiResponse<string>>;

