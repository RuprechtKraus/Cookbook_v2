using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.RecipeStepModel
{
    public class RecipeStep : Entity
    {
        public int RecipeId { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }
    }
}
