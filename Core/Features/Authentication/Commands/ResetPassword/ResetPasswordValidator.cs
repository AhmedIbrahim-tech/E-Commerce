namespace Core.Features.Authentication.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        ApplyValidationRoles();
    }

    public void ApplyValidationRoles()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .EmailAddress().WithMessage(SharedResourcesKeys.InvalidFormat);

        RuleFor(c => c.NewPassword)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .MinimumLength(6).WithMessage(SharedResourcesKeys.MinLengthIs6)
                .NotNull().WithMessage(SharedResourcesKeys.Required);

        RuleFor(c => c.ConfirmPassword)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .Equal(c => c.NewPassword).WithMessage(SharedResourcesKeys.PasswordsDoNotMatch);
    }
}

