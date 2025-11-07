namespace Core.Features.Reviews.Queries.GetReviewPaginatedList;

public class GetReviewPaginatedListValidator : AbstractValidator<GetReviewPaginatedListQuery>
{
    public GetReviewPaginatedListValidator()
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

