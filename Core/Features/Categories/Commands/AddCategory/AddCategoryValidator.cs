namespace Core.Features.Categories.Commands.AddCategory;

public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
{
    private readonly ICategoryService _categoryService;

    public AddCategoryValidator(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        ApplyValidationRules();
        ApplyCustomValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);
        RuleFor(c => c.Description)
            .MaximumLength(300).WithMessage(SharedResourcesKeys.MaxLengthIs300);
    }

    public void ApplyCustomValidationRules()
    {
        RuleFor(c => c.Name)
            .MustAsync(async (name, cancellation) => !await _categoryService.IsNameExist(name))
            .WithMessage(SharedResourcesKeys.IsExist);
    }
}

