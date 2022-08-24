using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Cookbook_v2.Toolkit;
using Cookbook_v2.Domain.UserModel;
using Microsoft.Extensions.Options;
using Cookbook_v2.Toolkit.Web.Abstractions;

namespace Cookbook_v2.Api.Authorization
{
    public class JwtUtils : IJwtUtils<User>
    {
        private readonly AuthenticationOptions _authOptions;

        public JwtUtils( IOptions<AuthenticationOptions> authOptions )
        {
            _authOptions = authOptions.Value;
        }

        public string GenerateToken( User user )
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = GetSecurityTokenDescriptor( user.Username );
            SecurityToken token = tokenHandler.CreateToken( tokenDescriptor );
            return tokenHandler.WriteToken( token );
        }

        public string ValidateToken( string token )
        {
            if ( token == null )
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken( token,
                    GetTokenValidationParameters(),
                    out SecurityToken validatedToken );

                var jwtToken = (JwtSecurityToken) validatedToken;
                var username = jwtToken.Claims
                    .SingleOrDefault( x => x.Type == "Username" ).Value;

                return username;
            }
            catch
            {
                return null;
            }
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor( string username )
        {
            var key = Encoding.ASCII.GetBytes( _authOptions.Secret );
            int expiration = _authOptions.Expiration;
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( new[] { new Claim( "Username", username ) } ),
                Expires = DateTime.UtcNow.AddDays( expiration ),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey( key ),
                    SecurityAlgorithms.HmacSha256Signature )
            };
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            var key = Encoding.ASCII.GetBytes( _authOptions.Secret );
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey( key ),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
