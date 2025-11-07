namespace Core.Features.Authentication.ConfirmResetPassword;

public class ConfirmResetPasswordValidator : AbstractValidator<ConfirmResetPasswordQuery>
{
    public ConfirmResetPasswordValidator()
    {
        ApplyValidationRoles();
    }

    public void ApplyValidationRoles()
    {
        RuleFor(c => c.Code)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

