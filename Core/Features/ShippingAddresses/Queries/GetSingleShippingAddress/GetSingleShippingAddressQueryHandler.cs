namespace Core.Features.ShippingAddresses.Queries.GetSingleShippingAddress;

public class GetSingleShippingAddressQueryHandler : ApiResponseHandler,
    IRequestHandler<GetSingleShippingAddressQuery, ApiResponse<GetSingleShippingAddressResponse>>
{
    private readonly IShippingAddressService _shippingAddressService;

    public GetSingleShippingAddressQueryHandler(IShippingAddressService shippingAddressService) : base()
    {
        _shippingAddressService = shippingAddressService;
    }

    public async Task<ApiResponse<GetSingleShippingAddressResponse>> Handle(GetSingleShippingAddressQuery request, CancellationToken cancellationToken)
    {
        var shippingAddress = await _shippingAddressService.GetShippingAddressByIdAsync(request.Id);
        if (shippingAddress is null) return NotFound<GetSingleShippingAddressResponse>(SharedResourcesKeys.ShippingAddressDoesNotExist);

        var shippingAddressResponse = new GetSingleShippingAddressResponse(
            shippingAddress.Id,
            shippingAddress.FirstName,
            shippingAddress.LastName,
            shippingAddress.Street,
            shippingAddress.City,
            shippingAddress.State
        );

        return Success(shippingAddressResponse);
    }
}

