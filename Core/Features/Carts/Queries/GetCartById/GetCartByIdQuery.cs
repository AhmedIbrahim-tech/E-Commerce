namespace Core.Features.Carts.Queries.GetCartById;

public record GetCartByIdQuery(Guid Id) : IRequest<ApiResponse<GetCartByIdResponse>>;

