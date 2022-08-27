using Cookbook_v2.Domain.Entities;

namespace Cookbook_v2.Domain.CategoryModel
{
    public class Category : EntityBase
    {
        public static readonly int s_maxNameLength = 50;
        public static readonly int s_maxDescriptionLength = 200;
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
    }
}
