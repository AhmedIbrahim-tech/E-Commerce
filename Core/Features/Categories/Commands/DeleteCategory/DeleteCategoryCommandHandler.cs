namespace Core.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteCategoryCommand, ApiResponse<string>>
{
    private readonly ICategoryService _categoryService;

    public DeleteCategoryCommandHandler(ICategoryService categoryService) : base()
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResponse<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(request.Id);
        if (category == null) return NotFound<string>();

        var result = await _categoryService.DeleteCategoryAsync(category);
        if (result == "Success") return Deleted<string>();
        return BadRequest<string>();
    }
}

