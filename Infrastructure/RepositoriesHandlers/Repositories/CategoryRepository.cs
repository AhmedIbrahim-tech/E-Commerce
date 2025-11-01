namespace Infrastructure.RepositoriesHandlers.Repositories;

public class CategoryRepository(ApplicationContext dbcontext) : GenericRepositoryAsync<Category>(dbcontext), ICategoryRepository
{
    private readonly DbSet<Category> _categories = dbcontext.Set<Category>();
}
