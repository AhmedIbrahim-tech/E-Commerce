namespace Core.Features.Orders.Commands.AddOrder;

public class AddOrderCommandHandler : ApiResponseHandler,
    IRequestHandler<AddOrderCommand, Guid>
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICurrentUserService _currentUserService;

    public AddOrderCommandHandler(
        IOrderService orderService,
        IProductService productService,
        ICartService cartService,
        ICurrentUserService currentUserService) : base()
    {
        _orderService = orderService;
        _productService = productService;
        _cartService = cartService;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        // Check if the user is authenticated
        if (!_currentUserService.IsAuthenticated)
            throw new InvalidOperationException(SharedResourcesKeys.PleaseLoginFirst);

        // Retrieve the cart
        var userId = _currentUserService.GetCartOwnerId();
        var cartKey = $"cart:{userId}";

        var result1 = await _cartService.MigrateGuestCartToCustomerAsync(userId);
        var badRequestMessage = result1 switch
        {
            "FailedInEditCart" => SharedResourcesKeys.FailedToModifyThisCart,
            "TransactionFailedToCommit" => SharedResourcesKeys.TransactionFailedToCommit,
            "FailedInMigrateGuestCartToCustomer" => SharedResourcesKeys.FailedInMigrateGuestCartToCustomer,
            "FailedToDeleteGuestIdCookie" => SharedResourcesKeys.FailedToDeleteGuestIdCookie,
            _ => null
        };

        if (badRequestMessage != null)
            BadRequest<string>(badRequestMessage);

        var cart = await _cartService.GetCartByKeyAsync(cartKey);
        if (cart == null || cart.CartItems?.Count == 0)
            throw new InvalidOperationException(SharedResourcesKeys.CartNotFoundOrEmpty);

        var order = new Order();

        // Validate and process each cart item
        foreach (var item in cart.CartItems!)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {item.ProductId} does not exist.");

            if (product.Price == null || product.StockQuantity < item.Quantity)
                throw new InvalidOperationException($"Product {product.Name} is not available or stock is insufficient.");

            order.OrderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                OrderId = order.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
                SubAmount = item.Quantity * (product.Price ?? 0)
            });
        }

        order.CustomerId = cart.CustomerId;
        order.OrderDate = DateTimeOffset.UtcNow.ToLocalTime();
        order.TotalAmount = order.OrderItems.Sum(i => i.SubAmount);
        order.Status = Status.Draft;

        // Add Order and return result
        var result2 = await _orderService.AddOrderAsync(order);
        if (result2 != "Success")
            throw new InvalidOperationException(SharedResourcesKeys.CreateFailed);
        return order.Id;
    }
}

