namespace Core.Features.ShippingAddresses.Commands.AddShippingAddress;

public class AddShippingAddressCommandHandler : ApiResponseHandler,
    IRequestHandler<AddShippingAddressCommand, ApiResponse<string>>
{
    private readonly IShippingAddressService _shippingAddressService;
    private readonly ICurrentUserService _currentUserService;

    public AddShippingAddressCommandHandler(
        IShippingAddressService shippingAddressService,
        ICurrentUserService currentUserService) : base()
    {
        _shippingAddressService = shippingAddressService;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<string>> Handle(AddShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetUserId();
        var shippingAddress = new ShippingAddress
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Street = request.Street,
            City = request.City,
            State = request.State,
            CustomerId = currentUserId
        };

        var result = await _shippingAddressService.AddShippingAddressAsync(shippingAddress);
        if (result == "Success") return Created("");
        return BadRequest<string>("CreateFailed");
    }
}

