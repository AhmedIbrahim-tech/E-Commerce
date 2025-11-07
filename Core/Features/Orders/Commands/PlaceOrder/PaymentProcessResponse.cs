namespace Core.Features.Orders.Commands.PlaceOrder;

public record PaymentProcessResponse(
    Guid OrderId,
    string PaymentUrl,
    string PaymentToken);

