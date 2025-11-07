namespace Core.Features.Carts.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler : ApiResponseHandler,
    IRequestHandler<RemoveFromCartCommand, ApiResponse<string>>
{
    private readonly ICartService _cartService;

    public RemoveFromCartCommandHandler(ICartService cartService) : base()
    {
        _cartService = cartService;
    }

    public async Task<ApiResponse<string>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var result = await _cartService.RemoveItemFromCartAsync(request.ProductId);
        return result switch
        {
            "Success" => Success<string>(SharedResourcesKeys.ItemRemovedFromCart),
            "CartNotFound" => NotFound<string>(SharedResourcesKeys.CartNotFoundOrEmpty),
            "ProductNotFound" => NotFound<string>(SharedResourcesKeys.ProductNotFound),
            "ItemNotFoundInCart" => NotFound<string>(SharedResourcesKeys.ItemNotFoundInCart),
            "FailedInRemoveItemFromCart" => BadRequest<string>(SharedResourcesKeys.FailedToModifyThisCart),
            _ => BadRequest<string>(SharedResourcesKeys.AnErrorOccurredWhileRemovingFromTheCart)
        };
    }
}

