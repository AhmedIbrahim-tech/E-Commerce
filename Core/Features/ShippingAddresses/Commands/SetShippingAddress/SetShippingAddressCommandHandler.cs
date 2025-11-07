namespace Core.Features.ShippingAddresses.Commands.SetShippingAddress;

public class SetShippingAddressCommandHandler : ApiResponseHandler,
    IRequestHandler<SetShippingAddressCommand, ApiResponse<string>>
{
    private readonly IShippingAddressService _shippingAddressService;
    private readonly IOrderService _orderService;

    public SetShippingAddressCommandHandler(
        IShippingAddressService shippingAddressService,
        IOrderService orderService) : base()
    {
        _shippingAddressService = shippingAddressService;
        _orderService = orderService;
    }

    public async Task<ApiResponse<string>> Handle(SetShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.OrderId);
        if (order == null || order.Status != Status.Draft)
            return BadRequest<string>("InvalidOrder");

        var shippingAddress = await _shippingAddressService.GetShippingAddressByIdAsync(request.ShippingAddressId);
        if (shippingAddress == null)
            return BadRequest<string>("ShippingAddressDoesNotExist");

        var deliveryOffset = DeliveryTimeCalculator.Calculate(shippingAddress.City, order.Delivery!.DeliveryMethod);
        var deliveryCost = DeliveryCostCalculator.Calculate(shippingAddress.City, order.Delivery.DeliveryMethod);

        order.ShippingAddressId = request.ShippingAddressId;
        order.Delivery.Description = $"Delivery for order #{order.Id} to {shippingAddress.State}, {shippingAddress.City}, {shippingAddress.Street}";
        order.Delivery.DeliveryTime = DateTime.UtcNow.Add(deliveryOffset);
        order.Delivery.Cost = deliveryCost;

        var result = await _orderService.EditOrderAsync(order);
        if (result != "Success")
            return BadRequest<string>("UpdateFailed");
        return Success("");
    }
}

