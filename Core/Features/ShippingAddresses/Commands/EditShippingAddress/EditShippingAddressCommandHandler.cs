namespace Core.Features.ShippingAddresses.Commands.EditShippingAddress;

public class EditShippingAddressCommandHandler : ApiResponseHandler,
    IRequestHandler<EditShippingAddressCommand, ApiResponse<string>>
{
    private readonly IShippingAddressService _shippingAddressService;

    public EditShippingAddressCommandHandler(IShippingAddressService shippingAddressService) : base()
    {
        _shippingAddressService = shippingAddressService;
    }

    public async Task<ApiResponse<string>> Handle(EditShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var shippingAddress = await _shippingAddressService.GetShippingAddressByIdAsync(request.Id);
        if (shippingAddress == null) return NotFound<string>("ShippingAddressDoesNotExist");

        shippingAddress.FirstName = request.FirstName;
        shippingAddress.LastName = request.LastName;
        shippingAddress.Street = request.Street;
        shippingAddress.City = request.City;
        shippingAddress.State = request.State;

        var result = await _shippingAddressService.EditShippingAddressAsync(shippingAddress);
        if (result == "Success") return Edit("");
        return BadRequest<string>("UpdateFailed");
    }
}

