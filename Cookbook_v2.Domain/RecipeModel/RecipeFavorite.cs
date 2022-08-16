using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Domain.RecipeModel
{
    public class RecipeFavorite
    {
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
