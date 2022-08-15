using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.RecipeModel
{
    public class RecipeStep : Entity
    {
        public int Index { get; set; }
        public string Description { get; set; }

        public int RecipeId { get; set; }
    }
}
