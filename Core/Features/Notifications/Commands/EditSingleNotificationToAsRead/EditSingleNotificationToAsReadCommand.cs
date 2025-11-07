namespace Core.Features.Notifications.Commands.EditSingleNotificationToAsRead;

public record EditSingleNotificationToAsReadCommand(string notificationId) : IRequest<ApiResponse<string>>;

