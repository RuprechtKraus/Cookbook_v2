namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public class RecipeIngredientsSection : EntityBase
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }

        public RecipeIngredientsSection( string title, string ingredients )
        {
            Title = title;
            Ingredients = ingredients;
        }

        // Workaround for EF
        protected RecipeIngredientsSection()
        {
        }
    }
}
