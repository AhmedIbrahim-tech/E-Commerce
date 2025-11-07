namespace Core.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : ApiResponseHandler,
    IRequestHandler<GetCategoryByIdQuery, ApiResponse<GetCategoryByIdResponse>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryByIdQueryHandler(ICategoryService categoryService) : base()
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResponse<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.Id);
        if (category is null) return NotFound<GetCategoryByIdResponse>(SharedResourcesKeys.CategoryNotFound);

        var categoryResponse = new GetCategoryByIdResponse(
            category.Id,
            category.Name!,
            category.Description
        );

        return Success(categoryResponse);
    }
}

