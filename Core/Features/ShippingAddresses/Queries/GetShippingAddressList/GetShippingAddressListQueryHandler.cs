namespace Core.Features.ShippingAddresses.Queries.GetShippingAddressList;

public class GetShippingAddressListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetShippingAddressListQuery, ApiResponse<List<GetShippingAddressListResponse>>>
{
    private readonly IShippingAddressService _shippingAddressService;
    private readonly ICurrentUserService _currentUserService;

    public GetShippingAddressListQueryHandler(
        IShippingAddressService shippingAddressService,
        ICurrentUserService currentUserService) : base()
    {
        _shippingAddressService = shippingAddressService;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<List<GetShippingAddressListResponse>>> Handle(GetShippingAddressListQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetUserId();
        var shippingAddressList = await _shippingAddressService.GetShippingAddressListByCustomerIdAsync(currentUserId);
        var shippingAddressListResponse = shippingAddressList
            .Select(address => new GetShippingAddressListResponse(
                address.Id,
                address.Street,
                address.City,
                address.State
            ))
            .ToList();

        return Success(shippingAddressListResponse);
    }
}

