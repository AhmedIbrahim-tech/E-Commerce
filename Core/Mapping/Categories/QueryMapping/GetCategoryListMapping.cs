using Core.Features.Categories.Queries.GetCategoryList;

namespace Core.Mapping.Categories;

public partial class CategoryProfile
{
    public void GetCategoryListMapping()
    {
        CreateMap<Category, GetCategoryListQuery>();
    }
}
