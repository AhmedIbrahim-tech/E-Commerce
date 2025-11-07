namespace Core.Features.Emails.Commands.SendEmail;

public class SendEmailValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .EmailAddress().WithMessage(SharedResourcesKeys.InvalidFormat);
    }
}

