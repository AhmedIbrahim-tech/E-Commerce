using Core.Features.Categories.Commands.AddCategory;

namespace Core.Mapping.Categories;

public partial class CategoryProfile
{
    public void AddCategoryCommandMapping()
    {
        CreateMap<AddCategoryCommand, Category>();
    }
}
