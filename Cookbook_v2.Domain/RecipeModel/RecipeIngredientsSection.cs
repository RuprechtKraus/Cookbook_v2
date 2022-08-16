using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.RecipeModel
{
    public class RecipeIngredientsSection : Entity
    {
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
    }
}
