namespace Core.Features.Deliveries.Commands.SetDeliveryMethod;

public class SetDeliveryMethodCommandHandler : ApiResponseHandler,
    IRequestHandler<SetDeliveryMethodCommand, ApiResponse<string>>
{
    private readonly IOrderService _orderService;

    public SetDeliveryMethodCommandHandler(IOrderService orderService) : base()
    {
        _orderService = orderService;
    }

    public async Task<ApiResponse<string>> Handle(SetDeliveryMethodCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.OrderId);
        if (order == null || order.Status != Status.Draft)
            return BadRequest<string>(SharedResourcesKeys.InvalidOrder);

        if (order.Delivery == null)
            order.Delivery = new Delivery();

        order.Delivery.DeliveryMethod = request.DeliveryMethod;
        order.Delivery.Status = Status.Draft;

        var result = await _orderService.EditOrderAsync(order);
        if (result != "Success")
            return BadRequest<string>(SharedResourcesKeys.UpdateFailed);
        return Success("");
    }
}

