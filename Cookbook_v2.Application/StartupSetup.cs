using Microsoft.Extensions.DependencyInjection;
using Cookbook_v2.Infrastructure.Services;
using Cookbook_v2.Infrastructure.Services.Interfaces;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Application.JsonWebTokenUtils;

namespace Cookbook_v2.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddAppliactionServices(
            this IServiceCollection services )
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddSingleton<IJwtUtils<User>, JwtUtils>();
        }
    }
}
