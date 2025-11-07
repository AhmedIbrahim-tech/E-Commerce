namespace Core.Features.Carts.Commands.UpdateItemQuantity;

public class UpdateItemQuantityCommandHandler : ApiResponseHandler,
    IRequestHandler<UpdateItemQuantityCommand, ApiResponse<string>>
{
    private readonly ICartService _cartService;

    public UpdateItemQuantityCommandHandler(ICartService cartService) : base()
    {
        _cartService = cartService;
    }

    public async Task<ApiResponse<string>> Handle(UpdateItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var result = await _cartService.UpdateItemQuantityAsync(request.ProductId, request.Quantity);
        return result switch
        {
            "Success" => Success<string>(SharedResourcesKeys.ItemQuantityUpdated),
            "CartNotFound" => NotFound<string>(SharedResourcesKeys.CartNotFoundOrEmpty),
            "ProductNotFound" => NotFound<string>(SharedResourcesKeys.ProductNotFound),
            "ItemNotFoundInCart" => NotFound<string>(SharedResourcesKeys.ItemNotFoundInCart),
            "FailedInUpdateItemQuantity" => BadRequest<string>(SharedResourcesKeys.FailedToModifyThisCart),
            _ => BadRequest<string>(SharedResourcesKeys.AnErrorOccurredWhileUpdatingItemQuantity)
        };
    }
}

