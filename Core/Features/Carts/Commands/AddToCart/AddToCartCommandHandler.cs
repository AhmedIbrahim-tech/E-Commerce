namespace Core.Features.Carts.Commands.AddToCart;

public class AddToCartCommandHandler : ApiResponseHandler,
    IRequestHandler<AddToCartCommand, ApiResponse<string>>
{
    private readonly ICartService _cartService;

    public AddToCartCommandHandler(ICartService cartService) : base()
    {
        _cartService = cartService;
    }

    public async Task<ApiResponse<string>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var result = await _cartService.AddToCartAsync(request.ProductId, request.Quantity);
        return result switch
        {
            "Success" => Success<string>(SharedResourcesKeys.AddedToCart),
            "ProductNotFound" => NotFound<string>(SharedResourcesKeys.ProductNotFound),
            "FailedInAddItemToCart" => BadRequest<string>(SharedResourcesKeys.FailedToModifyThisCart),
            "ItemAlreadyExistsInCart" => BadRequest<string>(SharedResourcesKeys.ItemAlreadyExistsInCart),
            _ => BadRequest<string>(SharedResourcesKeys.AnErrorOccurredWhileAddingToTheCart)
        };
    }
}

