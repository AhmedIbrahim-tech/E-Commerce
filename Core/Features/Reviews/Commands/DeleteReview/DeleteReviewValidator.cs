namespace Core.Features.Reviews.Commands.DeleteReview;

public class DeleteReviewValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

