using Cookbook_v2.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cookbook_v2.Application.Extensions
{
    public static class ImagesSettingsExtension
    {
        public static IServiceCollection ConfigureImagesSettings(
            this IServiceCollection services, IConfiguration configuration )
        {
            IConfigurationSection section = configuration.GetSection( "ImagesSettings" );
            return services.Configure<ImagesSettings>( ( x ) =>
            {
                x.RecipeImagesDirectory = section[ "RecipeImagesDirectory" ];
            } );
        }
    }
}
