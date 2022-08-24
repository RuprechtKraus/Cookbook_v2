using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cookbook_v2.Api.MessageContracts.UserModel;
using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Toolkit.Domain.Abstractions;
using Cookbook_v2.Toolkit.Exceptions;
using Cookbook_v2.Api.Authorization.Attributes;
using Cookbook_v2.Toolkit.Web.Abstractions;

namespace Cookbook_v2.Api.Controllers
{
    [CookbookAuthorize]
    [ApiController]
    [Route( "api/[controller]" )]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtUtils<User> _jwtUtils;

        public UserController( 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork,
            IJwtUtils<User> jwtUtils)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
        }

        [CookbookAllowAnonymous]
        [HttpPost( "register" )]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserRegisterCommand registerCommand )
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

            User user = Domain.UserModel.User.Builder
                .CreateUser(
                    registerCommand.Name,
                    registerCommand.Username,
                    registerCommand.About,
                    registerCommand.Password );

            await _userRepository.Add( user );
            await _unitOfWork.SaveAsync();

            return Ok( user.Id );
        }

        [CookbookAllowAnonymous]
        [HttpPost( "authenticate" )]
        public async Task<IActionResult> AuthenticateUser(
            [FromBody] UserAuthenticateCommand authCommand )
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

            UserAuthenticatedResponse response = new UserAuthenticatedResponse
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Token = _jwtUtils.GenerateToken( user )
            };

            return Ok( response );
        }
    }
}
