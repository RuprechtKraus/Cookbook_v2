using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cookbook_v2.Api.MessageContracts.UserModel;
using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Toolkit.Domain.Abstractions;
using Cookbook_v2.Toolkit.Exceptions;
using Cookbook_v2.Api.Authorization.Attributes;
using Cookbook_v2.Toolkit.Web.Abstractions;
using Cookbook_v2.Infrastructure.Services.Abstractions;

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

        [HttpGet( "{id}" )]
        public async Task<IActionResult> Get( int id )
        {
            User user = await _userService.GetById( id );
            return Ok( user );
        }

        [CookbookAllowAnonymous]
        [HttpPost( "register" )]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserRegisterCommand registerCommand )
        {
            User user = await _userService.RegisterUser( registerCommand );
            return Ok( user.Id );
        }

        [CookbookAllowAnonymous]
        [HttpPost( "authenticate" )]
        public async Task<IActionResult> AuthenticateUser(
            [FromBody] UserAuthenticateCommand authCommand )
        {
            UserAuthenticatedResponse authUser = await _userService.AuthenticateUser( authCommand );
            return Ok( authUser );
        }
    }
}
