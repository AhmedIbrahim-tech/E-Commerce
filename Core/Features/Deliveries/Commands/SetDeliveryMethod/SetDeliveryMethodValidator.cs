namespace Core.Features.Deliveries.Commands.SetDeliveryMethod;

public class SetDeliveryMethodValidator : AbstractValidator<SetDeliveryMethodCommand>
{
    public SetDeliveryMethodValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.OrderId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

