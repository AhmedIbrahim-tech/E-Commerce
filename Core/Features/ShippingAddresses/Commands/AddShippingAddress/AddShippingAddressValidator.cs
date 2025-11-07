namespace Core.Features.ShippingAddresses.Commands.AddShippingAddress;

public class AddShippingAddressValidator : AbstractValidator<AddShippingAddressCommand>
{
    private readonly IShippingAddressService _shippingAddressService;

    public AddShippingAddressValidator(IShippingAddressService shippingAddressService)
    {
        _shippingAddressService = shippingAddressService;
        ApplyValidationRules();
        ApplyCustomValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(c => c.Street)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(c => c.City)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(c => c.State)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);
    }

    public void ApplyCustomValidationRules()
    {
        RuleFor(c => c)
            .MustAsync(async (shippingAddress, cancellation) => !await _shippingAddressService.IsShippingAddressExist(shippingAddress.Street!, shippingAddress.City!, shippingAddress.State!))
            .WithMessage(SharedResourcesKeys.ShippingAddressIsExist);
    }
}

