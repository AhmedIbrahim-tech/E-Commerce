namespace Core.Features.Carts.Queries.GetMyCart;

public class GetMyCartQueryHandler : ApiResponseHandler,
    IRequestHandler<GetMyCartQuery, ApiResponse<GetMyCartResponse>>
{
    private readonly ICartService _cartService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IProductService _productService;

    public GetMyCartQueryHandler(ICartService cartService,
        ICurrentUserService currentUserService,
        IProductService productService) : base()
    {
        _cartService = cartService;
        _currentUserService = currentUserService;
        _productService = productService;
    }

    public async Task<ApiResponse<GetMyCartResponse>> Handle(GetMyCartQuery request, CancellationToken cancellationToken)
    {
        // 1. Get cart from cache
        var cart = await _cartService.GetMyCartAsync();
        if (cart is null) return NotFound<GetMyCartResponse>(SharedResourcesKeys.CartNotFoundOrEmpty);

        // 2. Extract ProductIds
        var productIds = cart.CartItems?.Select(i => i.ProductId).ToList() ?? new List<Guid>();

        // 3. Query DB to get product names
        var products = await _productService.GetProductsByIdsAsync(productIds);

        // 4. Map response using Select()
        var cartResponse = new GetMyCartResponse
        {
            CreatedAt = cart.CreatedAt,
            CustomerId = cart.CustomerId,
            CartItems = cart.CartItems?.Select(item => new CartItemResponse
            {
                ProductId = item.ProductId,
                ProductName = products.TryGetValue(item.ProductId, out var name) ? name : null,
                Quantity = item.Quantity,
                CreatedAt = item.CreatedAt
            }).ToList()
        };

        // 5. Return response
        var resultCart = cartResponse ?? new GetMyCartResponse { CustomerId = _currentUserService.GetUserId(), CreatedAt = DateTimeOffset.UtcNow };
        return Success(resultCart);
    }
}

