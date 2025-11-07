namespace Core.Features.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : IRequest<ApiResponse<string>>;

