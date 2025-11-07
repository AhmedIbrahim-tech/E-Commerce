namespace Core.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : ApiResponseHandler,
    IRequestHandler<GetOrderByIdQuery, ApiResponse<GetOrderByIdResponse>>
{
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;

    public GetOrderByIdQueryHandler(
        IOrderService orderService,
        IOrderItemService orderItemService) : base()
    {
        _orderService = orderService;
        _orderItemService = orderItemService;
    }

    public async Task<ApiResponse<GetOrderByIdResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.Id);
        if (order is null)
            return NotFound<GetOrderByIdResponse>(SharedResourcesKeys.NotFound);

        var orderResponse = new GetOrderByIdResponse
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            OrderStatus = order.Status,
            TotalAmount = order.TotalAmount,
            CustomerName = order.Customer != null ? $"{order.Customer.FirstName} {order.Customer.LastName}".Trim() : null,
            ShippingAddress = order.ShippingAddress != null ? $"{order.ShippingAddress.Street}, {order.ShippingAddress.City}, {order.ShippingAddress.State}" : null,
            PaymentMethod = order.Payment?.PaymentMethod,
            PaymentDate = order.Payment?.PaymentDate,
            PaymentStatus = order.Payment?.Status,
            DeliveryMethod = order.Delivery?.DeliveryMethod,
            DeliveryTime = order.Delivery?.DeliveryTime,
            DeliveryCost = order.Delivery?.Cost
        };

        Expression<Func<OrderItem, OrderItemResponse>> expression = orderItem => new OrderItemResponse(
            orderItem.ProductId,
            orderItem.Product != null ? orderItem.Product.Name : null,
            orderItem.Quantity,
            orderItem.UnitPrice
        );

        var orderItemsQueryable = _orderItemService.GetOrderItemsByOrderIdQueryable(request.Id);
        var orderItemPaginatedList = await orderItemsQueryable.Select(expression).ToPaginatedListAsync(request.OrderPageNumber, request.OrderPageSize);
        orderResponse.OrderItems = orderItemPaginatedList;

        return Success(orderResponse);
    }
}

