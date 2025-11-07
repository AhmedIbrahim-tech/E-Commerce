namespace Core.Features.Authorization.Queries.GetRoleById;

public record GetRoleByIdQuery(Guid Id) : IRequest<ApiResponse<GetRoleByIdResponse>>;

