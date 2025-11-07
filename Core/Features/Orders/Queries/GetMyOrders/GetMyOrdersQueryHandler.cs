namespace Core.Features.Orders.Queries.GetMyOrders;

public class GetMyOrdersQueryHandler : ApiResponseHandler,
    IRequestHandler<GetMyOrdersQuery, PaginatedResult<GetMyOrdersResponse>>
{
    private readonly IOrderService _orderService;
    private readonly ICurrentUserService _currentUserService;

    public GetMyOrdersQueryHandler(
        IOrderService orderService,
        ICurrentUserService currentUserService) : base()
    {
        _orderService = orderService;
        _currentUserService = currentUserService;
    }

    public async Task<PaginatedResult<GetMyOrdersResponse>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        Expression<Func<Order, GetMyOrdersResponse>> expression = o => new GetMyOrdersResponse(
            o.Id,
            o.OrderDate,
            o.Status,
            o.TotalAmount,
            o.Customer != null ? o.Customer.FirstName + " " + o.Customer.LastName : null,
            o.ShippingAddress != null ? $"{o.ShippingAddress.Street}, {o.ShippingAddress.City}, {o.ShippingAddress.State}" : null,
            o.Payment!.PaymentMethod,
            o.Payment.PaymentDate,
            o.Payment.Status,
            o.Delivery!.DeliveryMethod,
            o.Delivery.DeliveryTime,
            o.Delivery.Cost,
            o.Delivery.Status);

        var filterQuery = _orderService.FilterOrderPaginatedByCustomerIdQueryable(request.SortBy, request.Search!, userId);
        var paginatedList = await filterQuery.Select(expression)
                                             .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        paginatedList.Meta = new { Count = paginatedList.Data.Count() };
        return paginatedList;
    }
}

