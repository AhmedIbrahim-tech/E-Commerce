namespace Core.Features.Employees.Commands.EditEmployee;

public class EditEmployeeValidator : AbstractValidator<EditEmployeeCommand>
{
    public EditEmployeeValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(e => e.LastName)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(50).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(e => e.UserName)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(50).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .EmailAddress().WithMessage(SharedResourcesKeys.InvalidFormat);

        RuleFor(e => e.Position)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);

        RuleFor(e => e.Salary)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .GreaterThan(0).WithMessage(SharedResourcesKeys.GreaterThanZero);

        RuleFor(e => e.Address)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(200).WithMessage(SharedResourcesKeys.MaxLengthIs200);
    }
}

