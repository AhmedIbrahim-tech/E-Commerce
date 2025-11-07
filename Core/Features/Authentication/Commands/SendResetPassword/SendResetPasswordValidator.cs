namespace Core.Features.Authentication.SendResetPassword;

public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
{
    public SendResetPasswordValidator()
    {
        ApplyValidationRoles();
    }

    public void ApplyValidationRoles()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

