namespace Core.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteProductCommand, ApiResponse<string>>
{
    private readonly IProductService _productService;

    public DeleteProductCommandHandler(IProductService productService) : base()
    {
        _productService = productService;
    }

    public async Task<ApiResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(request.ProductId);
        if (product == null) return NotFound<string>(SharedResourcesKeys.ProductNotFound);
        var result = await _productService.DeleteProductAsync(product);
        if (result == "Success") return Deleted<string>();
        return BadRequest<string>(SharedResourcesKeys.DeleteFailed);
    }
}

