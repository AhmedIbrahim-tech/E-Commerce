namespace Core.Features.Categories.Queries.GetCategoryList;

public record GetCategoryListQuery : IRequest<ApiResponse<List<GetCategoryListResponse>>>;

