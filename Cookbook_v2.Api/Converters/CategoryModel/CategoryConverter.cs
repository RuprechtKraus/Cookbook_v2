using Cookbook_v2.Domain.CategoryModel;
using Cookbook_v2.Api.MessageContracts.CategoryModel;

namespace Cookbook_v2.Api.Converters.CategoryModel
{
    public static class CategoryConverter
    {
        public static CategoryDto ToDto( this Category category )
        {
            return new CategoryDto
            {
                Name = category.Name,
                Description = category.Description,
                IconName = category.IconName
            };
        }
    }
}
