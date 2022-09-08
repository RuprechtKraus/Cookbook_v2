using Cookbook_v2.Application.Commands.UserModel;
using Cookbook_v2.Application.Responses.UserModel;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.UserModel;

namespace Cookbook_v2.Application.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User> GetById( int id );
        Task<User> GetByUsername( string username );
        Task<User> RegisterUser( RegisterUserCommand registerCommand );
        Task<AuthenticateUserResponse> AuthenticateUser( 
            AuthenticateUserCommand authenticateCommand );
    }
}
