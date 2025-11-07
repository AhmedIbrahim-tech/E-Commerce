namespace Core.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : ApiResponseHandler,
    IRequestHandler<GetProductByIdQuery, ApiResponse<GetProductByIdResponse>>
{
    private readonly IProductService _productService;
    private readonly IReviewService _reviewService;

    public GetProductByIdQueryHandler(IProductService productService,
        IReviewService reviewService) : base()
    {
        _productService = productService;
        _reviewService = reviewService;
    }

    public async Task<ApiResponse<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(request.ProductId);
        if (product is null) return NotFound<GetProductByIdResponse>(SharedResourcesKeys.NotFound);

        var productResponse = new GetProductByIdResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.ImageURL,
            product.CreatedAt,
            product.Category?.Name
        );

        Expression<Func<Review, ReviewResponse>> expression = review => new ReviewResponse(
            review.CustomerId,
            review.Customer != null ? review.Customer.FirstName + " " + review.Customer.LastName : null,
            review.Rating,
            review.Comment
        );

        var reviewsQueryable = _reviewService.FilterReviewPaginatedQueryable(request.SortBy, request.Search!, request.ProductId);
        var reviewPaginatedList = await reviewsQueryable.Select(expression)
                                                        .ToPaginatedListAsync(request.ReviewPageNumber, request.ReviewPageSize);
        productResponse.Reviews = reviewPaginatedList;

        return Success(productResponse);
    }
}

