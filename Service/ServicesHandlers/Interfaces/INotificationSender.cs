namespace Service.ServicesHandlers.Interfaces;

public interface INotificationSender
{
    Task SendToUserAsync(string userId, string notification);
}
