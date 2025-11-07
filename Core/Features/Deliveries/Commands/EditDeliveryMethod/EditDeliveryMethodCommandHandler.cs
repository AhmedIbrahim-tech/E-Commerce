namespace Core.Features.Deliveries.Commands.EditDeliveryMethod;

public class EditDeliveryMethodCommandHandler : ApiResponseHandler,
    IRequestHandler<EditDeliveryMethodCommand, ApiResponse<string>>
{
    private readonly IOrderService _orderService;

    public EditDeliveryMethodCommandHandler(IOrderService orderService) : base()
    {
        _orderService = orderService;
    }

    public async Task<ApiResponse<string>> Handle(EditDeliveryMethodCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.OrderId);
        if (order == null || order.Status != Status.Draft)
            return BadRequest<string>(SharedResourcesKeys.InvalidOrder);

        if (order.Delivery == null)
            return NotFound<string>(SharedResourcesKeys.NotFound);

        order.Delivery.DeliveryMethod = request.DeliveryMethod;
        order.Delivery.Status = Status.Draft;

        var result = await _orderService.EditOrderAsync(order);
        if (result != "Success")
            return BadRequest<string>(SharedResourcesKeys.UpdateFailed);
        return Success("");
    }
}

