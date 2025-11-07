namespace Core.Features.Customers.Commands.EditCustomer;

public class EditCustomerValidator : AbstractValidator<EditCustomerCommand>
{
    public EditCustomerValidator()
    {
        ApplyValidationRules();
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
            .MaximumLength(50).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(c => c.UserName)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(50).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .EmailAddress().WithMessage(SharedResourcesKeys.InvalidFormat);
    }
}

