namespace Core.Features.Carts.Commands.DeleteCart;

public class DeleteCartCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteCartCommand, ApiResponse<string>>
{
    private readonly ICartService _cartService;

    public DeleteCartCommandHandler(ICartService cartService) : base()
    {
        _cartService = cartService;
    }

    public async Task<ApiResponse<string>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var result = await _cartService.DeleteCartAsync(request.CartId);
        if (result) return Deleted<string>();
        return BadRequest<string>(SharedResourcesKeys.DeleteFailed);
    }
}

