namespace Core.Features.ShippingAddresses.Commands.SetShippingAddress;

public class SetShippingAddressValidator : AbstractValidator<SetShippingAddressCommand>
{
    public SetShippingAddressValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.OrderId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

        RuleFor(c => c.ShippingAddressId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

