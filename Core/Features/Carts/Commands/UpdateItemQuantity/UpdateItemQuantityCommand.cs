namespace Core.Features.Carts.Commands.UpdateItemQuantity;

public record UpdateItemQuantityCommand(Guid ProductId, int Quantity) : IRequest<ApiResponse<string>>;

