namespace Infrastructure.RepositoriesHandlers.Repositories;

public class OrderRepository(ApplicationContext dbContext) : GenericRepositoryAsync<Order>(dbContext), IOrderRepository
{
    #region Fields
    private readonly DbSet<Order> _orders = dbContext.Set<Order>();
    #endregion

    #region Handle Functions

    #endregion
}
