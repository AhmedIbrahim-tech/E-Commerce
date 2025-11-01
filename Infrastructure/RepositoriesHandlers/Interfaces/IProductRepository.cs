namespace Infrastructure.RepositoriesHandlers.Interfaces;

public interface IProductRepository : IGenericRepositoryAsync<Product>
{
    Task<Dictionary<Guid, string?>> GetProductsByIdsAsync(List<Guid> productIds);
}
