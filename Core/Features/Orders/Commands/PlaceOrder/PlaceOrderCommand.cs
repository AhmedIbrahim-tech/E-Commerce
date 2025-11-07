namespace Core.Features.Orders.Commands.PlaceOrder;

public record PlaceOrderCommand(Guid OrderId) : IRequest<ApiResponse<PaymentProcessResponse>>;

