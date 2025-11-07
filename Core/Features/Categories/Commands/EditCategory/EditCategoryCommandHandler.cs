namespace Core.Features.Categories.Commands.EditCategory;

public class EditCategoryCommandHandler : ApiResponseHandler,
    IRequestHandler<EditCategoryCommand, ApiResponse<string>>
{
    private readonly ICategoryService _categoryService;

    public EditCategoryCommandHandler(ICategoryService categoryService) : base()
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResponse<string>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.Id);
        if (category == null) return NotFound<string>();

        category.Name = request.Name;
        category.Description = request.Description;

        var result = await _categoryService.EditCategoryAsync(category);
        if (result == "Success") return Edit("");
        return BadRequest<string>();
    }
}

