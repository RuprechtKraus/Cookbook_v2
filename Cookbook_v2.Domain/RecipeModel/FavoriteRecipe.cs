using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Domain.RecipeModel
{
    public class FavoriteRecipe
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }

        public FavoriteRecipe( User user, Recipe recipe )
        {
            User = user;
            UserId = user.Id;
            Recipe = recipe;
            RecipeId = recipe.Id;
        }

        protected FavoriteRecipe()
        {
        }
    }
}
