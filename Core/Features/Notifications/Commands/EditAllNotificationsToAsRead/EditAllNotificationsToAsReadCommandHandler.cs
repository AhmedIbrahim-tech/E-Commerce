namespace Core.Features.Notifications.Commands.EditAllNotificationsToAsRead;

public class EditAllNotificationsToAsReadCommandHandler : ApiResponseHandler,
    IRequestHandler<EditAllNotificationsToAsReadCommand, ApiResponse<string>>
{
    private readonly INotificationsService _notificationsService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EditAllNotificationsToAsReadCommandHandler(
        INotificationsService notificationsService,
        IHttpContextAccessor httpContextAccessor) : base()
    {
        _notificationsService = notificationsService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<string>> Handle(EditAllNotificationsToAsReadCommand request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return Unauthorized<string>(SharedResourcesKeys.UnAuthorized);

        var role = user?.FindFirst(ClaimTypes.Role)?.Value;
        var userId = user?.FindFirst(nameof(UserClaimModel.Id))?.Value;
        var result = role switch
        {
            "Admin" => await _notificationsService.MarkAllAsRead(userId!, NotificationReceiverType.Admin),
            "Employee" => await _notificationsService.MarkAllAsRead(userId!, NotificationReceiverType.Employee),
            "Customer" => await _notificationsService.MarkAllAsRead(userId!, NotificationReceiverType.Customer),
            _ => await _notificationsService.MarkAllAsRead(userId!, NotificationReceiverType.Unknowen),
        };

        if (result != "Success") return BadRequest<string>(SharedResourcesKeys.FailedToMarkAllNotificationsAsRead);
        return Success("");
    }
}

