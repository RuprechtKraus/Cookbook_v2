using Cookbook_v2.Application.Dtos.UserModel;
using Cookbook_v2.Domain.Entities.UserModel;

namespace Cookbook_v2.Application.Helpers.Converters
{
    public static class UserConverter
    {
        public static UserDetailsDto ToUserDetailsDto( this User user )
        {
            return new UserDetailsDto
            {
                Name = user.Name,
                Username = user.Username,
                About = user.About,
                RecipesCount = user.RecipesCount,
                LikesCount = user.LikesCount,
                FavoritesCount = user.FavoritesCount,
            };
        }
    }
}
