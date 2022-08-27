using System.Threading.Tasks;
using Cookbook_v2.Api.MessageContracts.UserModel;
using Cookbook_v2.Domain.RecipeModel;
using Cookbook_v2.Domain.UserModel;

namespace Cookbook_v2.Infrastructure.Services.Abstractions
{
    public interface IUserService
    {
        public Task<User> GetById( int id );
        public Task<User> GetByUsername( string username );
        public Task AddFavoriteRecipe( FavoriteRecipe favRecipe );
        public Task RemoveFavoriteRecipe( FavoriteRecipe favRecipe );
        public Task<User> RegisterUser( UserRegisterDto registerCommand );
        public Task<UserAuthenticatedDto> AuthenticateUser( UserAuthenticateDto authCommand );
    }
}
