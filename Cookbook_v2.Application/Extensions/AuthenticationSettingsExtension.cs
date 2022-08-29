using System.Net.Sockets;
using Cookbook_v2.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cookbook_v2.Application.Extensions
{
    public static class AuthenticationSettingsExtension
    {
        public static IServiceCollection ConfigureAuthenticationSettings(
            this IServiceCollection services, IConfiguration configuration )
        {
            IConfigurationSection section = configuration.GetSection( "AuthenticationSettings" );
            return services.Configure<AuthenticationSettings>((x) =>
            {
                x.Secret = section[ "Secret" ];
                x.ExpirationInDays = int.Parse( section[ "ExpirationInDays" ] );
            } );
        }
    }
}
