namespace Cookbook_v2.Application.Dtos.UserModel
{
    public class UserDetailsDto
    {
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string About { get; set; } = "";
        public int RecipesCount { get; set; }
        public int LikesCount { get; set; }
        public int FavoritesCount { get; set; }
    }
}
