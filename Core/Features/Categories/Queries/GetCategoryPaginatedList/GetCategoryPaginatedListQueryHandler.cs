namespace Core.Features.Categories.Queries.GetCategoryPaginatedList;

public class GetCategoryPaginatedListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetCategoryPaginatedListQuery, PaginatedResult<GetCategoryPaginatedListResponse>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryPaginatedListQueryHandler(ICategoryService categoryService) : base()
    {
        _categoryService = categoryService;
    }

    public async Task<PaginatedResult<GetCategoryPaginatedListResponse>> Handle(GetCategoryPaginatedListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Category, GetCategoryPaginatedListResponse>> expression = c => new GetCategoryPaginatedListResponse(
            c.Id,
            c.Name!,
            c.Description
        );

        var filterQuery = _categoryService.FilterCategoryPaginatedQueryable(request.SortBy, request.Search!);
        var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
        paginatedList.Meta = new { Count = paginatedList.Data.Count() };
        return paginatedList;
    }
}

