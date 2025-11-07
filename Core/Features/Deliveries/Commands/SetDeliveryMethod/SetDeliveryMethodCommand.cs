namespace Core.Features.Deliveries.Commands.SetDeliveryMethod;

public record SetDeliveryMethodCommand(Guid OrderId, DeliveryMethod DeliveryMethod) : IRequest<ApiResponse<string>>;

