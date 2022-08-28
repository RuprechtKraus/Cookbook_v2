namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public class RecipeIngredientsSection : EntityBase
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }

        // Workaround for EF
        protected RecipeIngredientsSection()
        {
        }
    }
}
