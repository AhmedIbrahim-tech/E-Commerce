namespace Core.Features.Payments.Commands.SetPaymentMethod;

public class SetPaymentMethodCommandHandler : ApiResponseHandler,
    IRequestHandler<SetPaymentMethodCommand, ApiResponse<string>>
{
    private readonly IOrderService _orderService;

    public SetPaymentMethodCommandHandler(IOrderService orderService) : base()
    {
        _orderService = orderService;
    }

    public async Task<ApiResponse<string>> Handle(SetPaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.OrderId);
        if (order == null || order.Status != Status.Draft)
            return BadRequest<string>("InvalidOrder");

        if (order.Payment == null)
            order.Payment = new Payment();

        order.Payment.PaymentMethod = request.PaymentMethod;
        order.Payment.Status = Status.Draft;
        var result = await _orderService.EditOrderAsync(order);
        if (result != "Success")
            return BadRequest<string>("UpdateFailed");
        return Success("");
    }
}

