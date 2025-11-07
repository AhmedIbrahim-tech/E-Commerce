namespace Core.Features.Notifications.Commands.EditSingleNotificationToAsRead;

public class EditSingleNotificationToAsReadValidator : AbstractValidator<EditSingleNotificationToAsReadCommand>
{
    public EditSingleNotificationToAsReadValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.notificationId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

