using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cookbook_v2.Api.Authorization.Attributes;
using Cookbook_v2.Application.Commands.UserModel;
using Cookbook_v2.Application.Responses.UserModel;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Application.Dtos.UserModel;
using Cookbook_v2.Application.Helpers.Converters;

namespace Cookbook_v2.Api.Controllers
{
    [CookbookAuthorize]
    [ApiController]
    [Route( "api/[controller]" )]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController( IUserService userService )
        {
            _userService = userService;
        }

        [CookbookAllowAnonymous]
        [HttpGet( "details/{id}" )]
        public async Task<IActionResult> Details( int id )
        {
            UserDetailsDto details = ( await _userService.GetById( id ) )
                .ToUserDetailsDto();
            return Ok( details );
        }

        [CookbookAllowAnonymous]
        [HttpPost( "register" )]
        public async Task<IActionResult> RegisterUser(
            [FromBody] RegisterUserCommand registerCommand )
        {
            User user = await _userService.RegisterUser( registerCommand );

            return Ok( user.Id );
        }

        [CookbookAllowAnonymous]
        [HttpPost( "authenticate" )]
        public async Task<IActionResult> AuthenticateUser(
            [FromBody] AuthenticateUserCommand authenticateCommand )
        {
            AuthenticateUserResponse authenticatedUser = 
                await _userService.AuthenticateUser( authenticateCommand );

            return Ok( authenticatedUser );
        }
    }
}
