namespace Core.Features.ShippingAddresses.Commands.DeleteShippingAddress;

public class DeleteShippingAddressValidator : AbstractValidator<DeleteShippingAddressCommand>
{
    public DeleteShippingAddressValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

