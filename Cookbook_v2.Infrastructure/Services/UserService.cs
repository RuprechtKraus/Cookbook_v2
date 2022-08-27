using Cookbook_v2.Api.MessageContracts.UserModel;
using Cookbook_v2.Domain.UserModel;
using System.Threading.Tasks;
using Cookbook_v2.Infrastructure.Services.Abstractions;
using Cookbook_v2.Toolkit.Exceptions;
using Cookbook_v2.Toolkit.Domain.Abstractions;
using Cookbook_v2.Toolkit.Web.Abstractions;
using System.Collections.Generic;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
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

        public async Task AddFavoriteRecipe( FavoriteRecipe favRecipe )
        {
            await _userRepository.AddFavoriteRecipe( favRecipe );
        }

        public async Task RemoveFavoriteRecipe( FavoriteRecipe favRecipe )
        {
            await _userRepository.RemoveFavoriteRecipe( favRecipe );
        }

        public async Task<User> RegisterUser( UserRegisterDto registerCommand )
        {
            FluentValidation.Results.ValidationResult validationResult =
                await new UserRegisterCommandValidator().ValidateAsync( registerCommand );

            if ( !validationResult.IsValid )
            {
                throw new RegistrationException( "Fields validation failed" );
            }

            if ( await _userRepository.GetByUsername( registerCommand.Username ) != null )
            {
                throw new RegistrationException( "Username is already taken" );
            }

            User user = User.Builder
                .CreateUser(
                    registerCommand.Name,
                    registerCommand.Username,
                    registerCommand.About,
                    registerCommand.Password );

            await _userRepository.Add( user );
            await _unitOfWork.SaveAsync();

            return user;
        }

        public async Task<UserAuthenticatedDto> AuthenticateUser( UserAuthenticateDto authCommand )
        {
            FluentValidation.Results.ValidationResult validationResult =
                await new UserAuthenticateCommandValidator().ValidateAsync( authCommand );

            if ( !validationResult.IsValid )
            {
                throw new AuthenticationException( "Fields validation failed" );
            }

            User user = await _userRepository.GetByUsername( authCommand.Username );

            if ( user == null || !BCrypt.Net.BCrypt.Verify( authCommand.Password, user.PasswordHash ) )
            {
                throw new AuthenticationException( "Incorrect username or password" );
            }

            UserAuthenticatedDto authUser = new UserAuthenticatedDto
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Token = _jwtUtils.GenerateToken( user )
            };

            return authUser;
        }
    }
}
