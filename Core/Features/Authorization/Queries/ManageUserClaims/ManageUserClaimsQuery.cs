using Domain.Responses;

namespace Core.Features.Authorization.Queries.ManageUserClaims;

public record ManageUserClaimsQuery(Guid UserId) : IRequest<ApiResponse<ManageUserClaimsResponse>>;

