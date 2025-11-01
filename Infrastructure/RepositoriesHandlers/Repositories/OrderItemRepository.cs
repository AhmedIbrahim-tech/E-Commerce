namespace Infrastructure.RepositoriesHandlers.Repositories;

public class OrderItemRepository(ApplicationContext dbContext) : GenericRepositoryAsync<OrderItem>(dbContext), IOrderItemRepository
{
}
