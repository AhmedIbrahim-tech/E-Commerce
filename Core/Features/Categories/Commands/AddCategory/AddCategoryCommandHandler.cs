namespace Core.Features.Categories.Commands.AddCategory;

public class AddCategoryCommandHandler : ApiResponseHandler,
    IRequestHandler<AddCategoryCommand, ApiResponse<string>>
{
    private readonly ICategoryService _categoryService;

    public AddCategoryCommandHandler(ICategoryService categoryService) : base()
    {
        _categoryService = categoryService;
    }

    public async Task<ApiResponse<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description
        };

        var result = await _categoryService.AddCategoryAsync(category);
        if (result == "Success") return Created("");
        return BadRequest<string>();
    }
}

