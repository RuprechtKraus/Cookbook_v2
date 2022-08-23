using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cookbook_v2.Api.Authorization;
using Cookbook_v2.Api.MessageContracts.UserModel;
using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Toolkit.Domain.Abstractions;
using Cookbook_v2.Toolkit.Exceptions;

namespace Cookbook_v2.Api.Controllers
{
    [CookbookAuthorize]
    [ApiController]
    [Route( "api/[controller]" )]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserController( IUserRepository userRepository, IUnitOfWork unitOfWork )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost( "register" )]
        [CookbookAllowAnonymous]
        public async Task<IActionResult> RegisterUser( [FromBody] UserRegisterCommand registerCommand )
        {
            FluentValidation.Results.ValidationResult validationResult = 
                await new UserRegisterCommandValidator().ValidateAsync( registerCommand );

            if ( !validationResult.IsValid )
            {
                throw new RegistrationException( validationResult.ToDictionary(), "Fields validation failed" );
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
    }
}
