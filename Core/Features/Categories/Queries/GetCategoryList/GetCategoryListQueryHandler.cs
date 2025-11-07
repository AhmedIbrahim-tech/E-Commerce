namespace Core.Features.Categories.Queries.GetCategoryList;

public class GetCategoryListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetCategoryListQuery, ApiResponse<List<GetCategoryListResponse>>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryListQueryHandler(ICategoryService categoryService) : base()
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResponse<List<GetCategoryListResponse>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categoryList = await _categoryService.GetCategoryListAsync();
        var categoryListResponse = categoryList
            .Where(c => c != null)
            .Select(category => new GetCategoryListResponse(
                category!.Id,
                category.Name!,
                category.Description
            ))
            .ToList();

        return Success(categoryListResponse);
    }
}

