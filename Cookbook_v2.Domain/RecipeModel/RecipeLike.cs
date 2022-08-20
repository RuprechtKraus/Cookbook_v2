using Cookbook_v2.Domain.UserModel;

namespace Cookbook_v2.Domain.RecipeModel
{
    public class RecipeLike
    {
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
