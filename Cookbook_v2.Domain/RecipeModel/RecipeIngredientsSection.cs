using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.RecipeModel
{
    public class RecipeIngredientsSection : Entity
    {
        public static readonly int s_titleMaxLength = 50;
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }

        protected RecipeIngredientsSection()
        {
        }
    }
}
