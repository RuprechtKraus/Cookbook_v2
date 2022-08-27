namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public class RecipeIngredientsSection : EntityBase
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
