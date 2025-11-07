namespace Core.Features.Deliveries.Commands.EditDeliveryMethod;

public class EditDeliveryMethodValidator : AbstractValidator<EditDeliveryMethodCommand>
{
    public EditDeliveryMethodValidator()
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

