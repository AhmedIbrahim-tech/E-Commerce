namespace Core.Features.Notifications.Commands.EditSingleNotificationToAsRead;

public class EditSingleNotificationToAsReadCommandHandler : ApiResponseHandler,
    IRequestHandler<EditSingleNotificationToAsReadCommand, ApiResponse<string>>
{
    private readonly INotificationsService _notificationsService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EditSingleNotificationToAsReadCommandHandler(
        INotificationsService notificationsService,
        IHttpContextAccessor httpContextAccessor) : base()
    {
        _notificationsService = notificationsService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<string>> Handle(EditSingleNotificationToAsReadCommand request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return Unauthorized<string>(SharedResourcesKeys.UnAuthorized);

        var role = user?.FindFirst(ClaimTypes.Role)?.Value;
        var userId = user?.FindFirst(nameof(UserClaimModel.Id))?.Value;
        var result = role switch
        {
            "Admin" => await _notificationsService.MarkAsRead(request.notificationId, userId!, NotificationReceiverType.Admin),
            "Employee" => await _notificationsService.MarkAsRead(request.notificationId, userId!, NotificationReceiverType.Employee),
            "Customer" => await _notificationsService.MarkAsRead(request.notificationId, userId!, NotificationReceiverType.Customer),
            _ => await _notificationsService.MarkAsRead(request.notificationId, userId!, NotificationReceiverType.Unknowen),
        };

        if (result != "Success") return BadRequest<string>(SharedResourcesKeys.FailedToMarkNotifyAsRead);
        return Success("");
    }
}

