namespace Core.Features.Notifications.Queries.GetNotificationPaginatedList;

public class GetNotificationPaginatedListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetNotificationPaginatedListQuery, ApiResponse<PaginatedResult<GetNotificationPaginatedListResponse>>>
{
    private readonly INotificationsService _notificationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetNotificationPaginatedListQueryHandler(
        INotificationsService notificationService,
        IHttpContextAccessor httpContextAccessor) : base()
    {
        _notificationService = notificationService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<PaginatedResult<GetNotificationPaginatedListResponse>>> Handle(GetNotificationPaginatedListQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return Unauthorized<PaginatedResult<GetNotificationPaginatedListResponse>>(SharedResourcesKeys.UnAuthorized);

        var role = user?.FindFirst(ClaimTypes.Role)?.Value;
        var userId = user?.FindFirst(nameof(UserClaimModel.Id))?.Value;
        var notifications = role switch
        {
            "Admin" => _notificationService.GetNotifications(userId!, NotificationReceiverType.Admin),
            "Employee" => _notificationService.GetNotifications(userId!, NotificationReceiverType.Employee),
            "Customer" => _notificationService.GetNotifications(userId!, NotificationReceiverType.Customer),
            _ => _notificationService.GetNotifications(userId!, NotificationReceiverType.Unknowen),
        };

        Expression<Func<NotificationResponse, GetNotificationPaginatedListResponse>> expression = c => new GetNotificationPaginatedListResponse(
            c.Id,
            c.ReceiverId,
            c.Message,
            c.CreatedAt,
            c.IsRead
        );

        var paginatedList = await notifications.Select(expression)
                                               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        return Success(paginatedList);
    }
}

