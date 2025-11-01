namespace Infrastructure.RepositoriesHandlers.Repositories;

public class ShippingAddressRepository(ApplicationContext dbContext) : GenericRepositoryAsync<ShippingAddress>(dbContext), IShippingAddressRepository
{
}
