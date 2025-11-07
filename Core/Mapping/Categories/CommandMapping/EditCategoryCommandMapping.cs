using Core.Features.Categories.Commands.EditCategory;

namespace Core.Mapping.Categories;

public partial class CategoryProfile
{
    public void EditCategoryCommandMapping()
    {
        CreateMap<EditCategoryCommand, Category>();
    }
}
