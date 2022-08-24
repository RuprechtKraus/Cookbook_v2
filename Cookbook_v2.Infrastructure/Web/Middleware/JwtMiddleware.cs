using System.Linq;
using System.Threading.Tasks;
using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Toolkit.Web.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Cookbook_v2.Infrastructure.Web.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware( RequestDelegate next )
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IUserRepository userRepository,
            IJwtUtils<User> jwtUtils )
        {
            var token = context.Request.Headers[ "Authorization" ].FirstOrDefault()?.Split( " " ).Last();
            var username = jwtUtils.ValidateToken( token );

            if ( username != null )
            {
                context.Items[ "User" ] = userRepository.GetByUsername( username );
            }

            await _next( context );
        }
    }
}
