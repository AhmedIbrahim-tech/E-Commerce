namespace Core.Features.ShippingAddresses.Commands.DeleteShippingAddress;

public class DeleteShippingAddressCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteShippingAddressCommand, ApiResponse<string>>
{
    private readonly IShippingAddressService _shippingAddressService;

    public DeleteShippingAddressCommandHandler(IShippingAddressService shippingAddressService) : base()
    {
        _shippingAddressService = shippingAddressService;
    }

    public async Task<ApiResponse<string>> Handle(DeleteShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var shippingAddress = await _shippingAddressService.GetShippingAddressByIdAsync(request.Id);
        if (shippingAddress == null) return NotFound<string>("ShippingAddressDoesNotExist");
        var result = await _shippingAddressService.DeleteShippingAddressAsync(shippingAddress);
        if (result == "Success") return Deleted<string>();
        return BadRequest<string>("DeleteFailed");
    }
}

