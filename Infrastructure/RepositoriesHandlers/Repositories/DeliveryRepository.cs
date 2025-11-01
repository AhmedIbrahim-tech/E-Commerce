namespace Infrastructure.RepositoriesHandlers.Repositories;

public class DeliveryRepository(ApplicationContext dbContext) : GenericRepositoryAsync<Delivery>(dbContext), IDeliveryRepository
{
}
