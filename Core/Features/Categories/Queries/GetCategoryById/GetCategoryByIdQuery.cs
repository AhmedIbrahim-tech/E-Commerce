namespace Core.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<ApiResponse<GetCategoryByIdResponse>>;

