namespace Core.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest<ApiResponse<string>>;

