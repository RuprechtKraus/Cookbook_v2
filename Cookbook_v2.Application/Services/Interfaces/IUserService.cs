using Cookbook_v2.Application.Commands.UserModel;
using Cookbook_v2.Application.Responses.UserModel;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.UserModel;

namespace Cookbook_v2.Application.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        public Task<User> GetById( int id );
        public Task<User> GetByUsername( string username );
        public Task AddFavoriteRecipe( FavoriteRecipe favRecipe );
        public Task RemoveFavoriteRecipe( FavoriteRecipe favRecipe );
        public Task<User> RegisterUser( RegisterUserCommand registerCommand );
        public Task<AuthenticateUserResponse> AuthenticateUser( 
            AuthenticateUserCommand authenticateCommand );
    }
}
