namespace Core.Features.Reviews.Commands.EditReview;

public class EditReviewValidator : AbstractValidator<EditReviewCommand>
{
    public EditReviewValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

        RuleFor(c => c.Rating)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

        RuleFor(c => c.Comment)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);
    }
}

