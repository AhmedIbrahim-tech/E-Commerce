namespace Core.Features.Carts.Commands.UpdateItemQuantity;

public class UpdateItemQuantityValidator : AbstractValidator<UpdateItemQuantityCommand>
{
    public UpdateItemQuantityValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

        RuleFor(c => c.Quantity)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .GreaterThan(0).WithMessage(SharedResourcesKeys.GreaterThanZero);
    }
}

