namespace Core.Features.Orders.Commands.PlaceOrder;

public class PlaceOrderCommandHandler : ApiResponseHandler,
    IRequestHandler<PlaceOrderCommand, ApiResponse<PaymentProcessResponse>>
{
    private readonly IOrderService _orderService;

    public PlaceOrderCommandHandler(IOrderService orderService) : base()
    {
        _orderService = orderService;
    }

    public async Task<ApiResponse<PaymentProcessResponse>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.OrderId);
        if (order == null) return NotFound<PaymentProcessResponse>(SharedResourcesKeys.OrderNotFound);
        var result = await _orderService.PlaceOrderAsync(order);
        return result switch
        {
            "CartIsEmpty" => NotFound<PaymentProcessResponse>(SharedResourcesKeys.CartNotFoundOrEmpty),
            "ProductNotFound" => NotFound<PaymentProcessResponse>(SharedResourcesKeys.ProductNotFound),
            "ErrorInPlacedOrder" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.ErrorInPlacedOrder),
            "NoCustomerFoundForOrder" => NotFound<PaymentProcessResponse>(SharedResourcesKeys.NoCustomerFoundForOrder),
            "FailedToConfirmCODOrder" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.FailedToConfirmCODOrder),
            "PaymentMethodNotSelected" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.PaymentMethodNotSelected),
            "PaymobIframeIDIsNotConfigured" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.PaymobIframeIDIsNotConfigured),
            "FailedToProcessPaymentForOrder" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.FailedToProcessPaymentForOrder),
            "PaymentTokenCannotBeNullOrEmpty" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.PaymentTokenCannotBeNullOrEmpty),
            "FailedToPersistOnlinePaymentData" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.FailedToPersistOnlinePaymentData),
            "FailedInDiscountQuantityFromStock" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.FailedInDiscountQuantityFromStock),
            "ShippingAddressIsRequiredForHomeDelivery" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.ShippingAddressIsRequiredForHomeDelivery),
            "InvalidPaymobIntegrationIDInConfiguration" => BadRequest<PaymentProcessResponse>(SharedResourcesKeys.InvalidPaymobIntegrationIDInConfiguration),
            _ => Success(new PaymentProcessResponse(order.Id, result, order.PaymentToken!))
        };
    }
}

