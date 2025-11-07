using Core.Features.Categories.Queries.GetCategoryById;

namespace Core.Mapping.Categories
{
    public partial class CategoryProfile
    {
        public void GetCategoryByIdMapping()
        {
            CreateMap<Category, GetCategoryByIdQuery>();
        }
    }
}
