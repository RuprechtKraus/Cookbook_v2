using Cookbook_v2.Domain.Entities;

namespace Cookbook_v2.Domain.CategoryModel
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }

        // Workaround for EF
        protected Category()
        {
        }
    }
}
