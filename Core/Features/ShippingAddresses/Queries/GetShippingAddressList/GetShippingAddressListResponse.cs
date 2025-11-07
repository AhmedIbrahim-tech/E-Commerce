namespace Core.Features.ShippingAddresses.Queries.GetShippingAddressList;

public record GetShippingAddressListResponse(Guid Id, string? Street, string? City, string? State);

