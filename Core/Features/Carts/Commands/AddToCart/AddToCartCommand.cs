namespace Core.Features.Carts.Commands.AddToCart;

public record AddToCartCommand(Guid ProductId, int Quantity) : IRequest<ApiResponse<string>>;

