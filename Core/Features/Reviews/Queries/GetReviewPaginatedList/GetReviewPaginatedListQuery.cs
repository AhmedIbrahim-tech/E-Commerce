namespace Core.Features.Reviews.Queries.GetReviewPaginatedList;

public record GetReviewPaginatedListQuery(Guid ProductId, int PageNumber, int PageSize, string? Search,
    ReviewSortingEnum SortBy) : IRequest<ApiResponse<PaginatedResult<GetReviewPaginatedListResponse>>>;

