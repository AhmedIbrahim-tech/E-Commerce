namespace Core.Features.Categories.Commands.EditCategory;

public record EditCategoryCommand(Guid Id, string Name, string? Description) : IRequest<ApiResponse<string>>;

