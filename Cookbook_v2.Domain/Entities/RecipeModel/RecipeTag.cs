namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public class RecipeTag
    {
        public int TagId { get; set; }
        public int RecipeId { get; set; }

        public RecipeTag( int tagId, int recipeId )
        {
            TagId = tagId;
            RecipeId = recipeId;
        }

        // Workaround for EF
        protected RecipeTag()
        {
        }
    }
}
