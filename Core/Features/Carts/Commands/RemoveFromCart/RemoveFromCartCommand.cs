namespace Core.Features.Carts.Commands.RemoveFromCart;

public record RemoveFromCartCommand(Guid ProductId) : IRequest<ApiResponse<string>>;

