namespace Core.Features.ShippingAddresses.Queries.GetSingleShippingAddress;

public record GetSingleShippingAddressQuery(Guid Id) : IRequest<ApiResponse<GetSingleShippingAddressResponse>>;

