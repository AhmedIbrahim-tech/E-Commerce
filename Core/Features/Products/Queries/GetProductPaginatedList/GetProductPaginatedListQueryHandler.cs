namespace Core.Features.Products.Queries.GetProductPaginatedList;

public class GetProductPaginatedListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetProductPaginatedListQuery, PaginatedResult<GetProductPaginatedListResponse>>
{
    private readonly IProductService _productService;

    public GetProductPaginatedListQueryHandler(IProductService productService) : base()
    {
        _productService = productService;
    }

    public async Task<PaginatedResult<GetProductPaginatedListResponse>> Handle(GetProductPaginatedListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Product, GetProductPaginatedListResponse>> expression = c => new GetProductPaginatedListResponse
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Price = c.Price,
            StockQuantity = c.StockQuantity,
            ImageURL = c.ImageURL,
            CreatedAt = c.CreatedAt,
            CategoryName = c.Category!.Name,
        };

        var filterQuery = _productService.FilterProductPaginatedQueryable(request.SortBy, request.Search!);
        var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
        paginatedList.Meta = new { Count = paginatedList.Data.Count() };
        return paginatedList;
    }
}

