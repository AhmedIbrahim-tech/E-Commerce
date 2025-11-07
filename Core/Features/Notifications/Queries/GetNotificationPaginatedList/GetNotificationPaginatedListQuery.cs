namespace Core.Features.Notifications.Queries.GetNotificationPaginatedList;

public record GetNotificationPaginatedListQuery(int PageNumber, int PageSize) :
    IRequest<ApiResponse<PaginatedResult<GetNotificationPaginatedListResponse>>>;

