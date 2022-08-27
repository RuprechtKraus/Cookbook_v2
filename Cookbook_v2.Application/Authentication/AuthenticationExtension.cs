using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cookbook_v2.Application.Authentication
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection ConfigureAuthenticationOptions(
            this IServiceCollection services, IConfiguration configuration )
        {
            IConfigurationSection options = configuration.GetSection( "AuthenticationOptions" );
            return services.Configure<AuthenticationOptions>( ( x ) =>
            {
                x.Secret = options[ "Secret" ];
                x.ExpirationInDays = int.Parse( options[ "ExpirationInDays" ] );
            } );
        }
    }
}
