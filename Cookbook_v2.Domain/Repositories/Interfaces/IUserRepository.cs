using System.Threading.Tasks;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Domain.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetById( int id );
        Task<User> GetByUsername( string username );
        Task Add( User user );
        Task Update( User user );
    }
}
