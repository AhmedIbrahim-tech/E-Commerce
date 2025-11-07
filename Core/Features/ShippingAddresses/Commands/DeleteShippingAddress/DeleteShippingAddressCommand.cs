namespace Core.Features.ShippingAddresses.Commands.DeleteShippingAddress;

public record DeleteShippingAddressCommand(Guid Id) : IRequest<ApiResponse<string>>;

