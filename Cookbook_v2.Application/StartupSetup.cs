using Microsoft.Extensions.DependencyInjection;
using Cookbook_v2.Application.Services;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Application.JsonWebTokenUtils;
using Microsoft.Extensions.Configuration;
using Cookbook_v2.Application.Settings;
using Cookbook_v2.Application.Extensions;

namespace Cookbook_v2.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddAppliactionServices(
            this IServiceCollection services )
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRecipeService, RecipeService>()
                .AddSingleton<IJwtUtils<User>, JwtUtils>();
        }

        public static IServiceCollection ConfigureApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration )
        {
            services.ConfigureAuthenticationSettings( configuration );
            services.ConfigureImagesSettings( configuration );
            return services;
        }
    }
}
