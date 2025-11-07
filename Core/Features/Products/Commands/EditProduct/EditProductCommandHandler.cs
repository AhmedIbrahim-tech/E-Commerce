namespace Core.Features.Products.Commands.EditProduct;

public class EditProductCommandHandler : ApiResponseHandler,
    IRequestHandler<EditProductCommand, ApiResponse<string>>
{
    private readonly IProductService _productService;

    public EditProductCommandHandler(IProductService productService) : base()
    {
        _productService = productService;
    }

    public async Task<ApiResponse<string>> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(request.Id);
        if (product == null) return NotFound<string>(SharedResourcesKeys.ProductNotFound);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;
        product.CategoryId = request.CategoryId;

        var result = await _productService.EditProductAsync(product);
        if (result == "Success") return Edit("");
        return BadRequest<string>(SharedResourcesKeys.UpdateFailed);
    }
}

