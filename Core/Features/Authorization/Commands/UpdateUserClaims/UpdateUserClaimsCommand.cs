using Domain.Requests;

namespace Core.Features.Authorization.Commands.UpdateUserClaims;

public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<ApiResponse<string>>
{
}

