namespace Core.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteOrderCommand, ApiResponse<string>>
{
    private readonly IOrderService _orderService;

    public DeleteOrderCommandHandler(IOrderService orderService) : base()
    {
        _orderService = orderService;
    }

    public async Task<ApiResponse<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.OrderId);
        if (order == null) return NotFound<string>(SharedResourcesKeys.OrderNotFound);
        var result = await _orderService.DeleteOrderAsync(order);
        return result != "Success" ? BadRequest<string>(SharedResourcesKeys.DeleteFailed) : Deleted<string>();
    }
}

