namespace Core.Features.Orders.Queries.GetOrderPaginatedList;

public class GetOrderPaginatedListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetOrderPaginatedListQuery, PaginatedResult<GetOrderPaginatedListResponse>>
{
    private readonly IOrderService _orderService;

    public GetOrderPaginatedListQueryHandler(IOrderService orderService) : base()
    {
        _orderService = orderService;
    }

    public async Task<PaginatedResult<GetOrderPaginatedListResponse>> Handle(GetOrderPaginatedListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Order, GetOrderPaginatedListResponse>> expression = o => new GetOrderPaginatedListResponse(
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

        var filterQuery = _orderService.FilterOrderPaginatedQueryable(request.SortBy, request.Search!);
        var paginatedList = await filterQuery.Select(expression)
                                             .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        paginatedList.Meta = new { Count = paginatedList.Data.Count() };
        return paginatedList;
    }
}

