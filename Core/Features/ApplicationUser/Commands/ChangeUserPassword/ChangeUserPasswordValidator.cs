namespace Core.Features.ApplicationUser.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator()
        {
            ApplyValidationRoles();
        }

        public void ApplyValidationRoles()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.Required);

            RuleFor(c => c.CurrentPassword)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.Required);

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
}

