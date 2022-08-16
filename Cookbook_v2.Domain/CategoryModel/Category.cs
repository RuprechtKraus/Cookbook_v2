using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.CategoryModel
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}
