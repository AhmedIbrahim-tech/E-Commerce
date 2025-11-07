namespace Core.Features.Authorization.Commands.AddRole;

public record AddRoleCommand(string RoleName) : IRequest<ApiResponse<string>>;

