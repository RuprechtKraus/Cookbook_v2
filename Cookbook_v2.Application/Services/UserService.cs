using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Domain.UoW.Interfaces;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Application.Commands.UserModel;
using Cookbook_v2.Application.Responses.UserModel;
using Cookbook_v2.Application.JsonWebTokenUtils;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Toolkit.Exceptions;

namespace Cookbook_v2.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtUtils<User> _jwtUtils;

        public UserService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IJwtUtils<User> jwtUtils )
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<User> GetById( int id )
        {
            User user = await _userRepository.GetById( id );
            return user ?? throw new KeyNotFoundException( "User not found" );
        }

        public async Task<User> GetByUsername( string username )
        {
            User user = await _userRepository.GetByUsername( username );
            return user ?? throw new KeyNotFoundException( "User not found" );
        }

        public async Task AddFavoriteRecipe( FavoriteRecipe favoriteRecipe )
        {
            await _userRepository.AddFavoriteRecipe( favoriteRecipe );
        }

        public Task RemoveFavoriteRecipe( FavoriteRecipe favoriteRecipe )
        {
            return _userRepository.RemoveFavoriteRecipe( favoriteRecipe );
        }

        public async Task<User> RegisterUser( RegisterUserCommand registerCommand )
        {
            if ( await _userRepository.GetByUsername( registerCommand.Username ) != null )
            {
                throw new RegistrationException( "Username is already taken" );
            }

            User user = User.Builder
                .CreateUser(
                    registerCommand.Name,
                    registerCommand.Username,
                    registerCommand.Password );

            await _userRepository.Add( user );
            await _unitOfWork.SaveAsync();

            return user;
        }

        public async Task<AuthenticateUserResponse> AuthenticateUser( 
            AuthenticateUserCommand authenticateCommand )
        {
            User user = await _userRepository.GetByUsername( authenticateCommand.Username );

            if ( user == null || !BCrypt.Net.BCrypt.Verify( 
                authenticateCommand.Password, 
                user.PasswordHash ) )
            {
                throw new AuthenticationException( "Incorrect username or password" );
            }

            string token = _jwtUtils.GenerateToken( user );

            AuthenticateUserResponse authenticatedUser = new AuthenticateUserResponse
            (
                user.Id,
                user.Name,
                user.Username,
                token
            );

            return authenticatedUser;
        }
    }
}
