namespace Core.Features.ShippingAddresses.Commands.SetShippingAddress;

public record SetShippingAddressCommand(Guid OrderId, Guid ShippingAddressId) : IRequest<ApiResponse<string>>;

