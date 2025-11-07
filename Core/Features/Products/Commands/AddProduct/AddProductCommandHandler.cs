namespace Core.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler : ApiResponseHandler,
    IRequestHandler<AddProductCommand, ApiResponse<string>>
{
    private readonly IProductService _productService;

    public AddProductCommandHandler(IProductService productService) : base()
    {
        _productService = productService;
    }

    public async Task<ApiResponse<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CategoryId = request.CategoryId
        };

        var result = await _productService.AddProductAsync(product, request.ImageURL!);
        return result switch
        {
            "NoImage" => BadRequest<string>(SharedResourcesKeys.NoImage),
            "FailedInAdd" => BadRequest<string>(SharedResourcesKeys.CreateFailed),
            "FailedToUploadImage" => BadRequest<string>(SharedResourcesKeys.FailedToUploadImage),
            _ => Created("")
        };
    }
}

