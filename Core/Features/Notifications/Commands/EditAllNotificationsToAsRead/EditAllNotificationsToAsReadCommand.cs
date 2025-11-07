namespace Core.Features.Notifications.Commands.EditAllNotificationsToAsRead;

public record EditAllNotificationsToAsReadCommand() : IRequest<ApiResponse<string>>;

