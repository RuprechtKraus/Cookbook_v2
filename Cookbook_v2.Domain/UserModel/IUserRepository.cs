using System.Threading.Tasks;
using Cookbook_v2.Domain.RecipeModel;
using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.UserModel
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetById( int id );
        Task<User> GetByUsername( string username );
        Task<int> Add( User user );
        Task AddFavoriteRecipe( FavoriteRecipe favRecipe );
        Task DeleteFavoriteRecipe( FavoriteRecipe favRecipe );
    }
}
