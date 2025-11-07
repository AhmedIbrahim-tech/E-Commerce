namespace Core.Features.ShippingAddresses.Queries.GetShippingAddressList;

public record GetShippingAddressListQuery : IRequest<ApiResponse<List<GetShippingAddressListResponse>>>;

