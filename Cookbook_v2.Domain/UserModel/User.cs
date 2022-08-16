using System.Collections.Generic;
using Cookbook_v2.Toolkit.Domain.Abstractions;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Domain.UserModel
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string About { get; set; }
        public int RecipesCount { get; set; }
        public int LikesCount { get; set; }
        public int FavoritesCount { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public virtual List<Recipe> Recipes { get; set; }
    }
}
