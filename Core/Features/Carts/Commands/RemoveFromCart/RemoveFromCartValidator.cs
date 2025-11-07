namespace Core.Features.Carts.Commands.RemoveFromCart;

public class RemoveFromCartValidator : AbstractValidator<RemoveFromCartCommand>
{
    public RemoveFromCartValidator()
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

