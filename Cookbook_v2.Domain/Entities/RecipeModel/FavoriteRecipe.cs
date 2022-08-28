using Cookbook_v2.Domain.Entities.UserModel;

namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public class FavoriteRecipe
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public FavoriteRecipe( int userId, int recipeId )
        {
            UserId = userId;
            RecipeId = recipeId;
        }

        // Workaround for EF
        protected FavoriteRecipe()
        {
        }
    }
}
