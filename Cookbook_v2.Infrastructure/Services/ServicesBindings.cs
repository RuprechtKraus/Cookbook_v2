using Cookbook_v2.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Cookbook_v2.Infrastructure.Services
{
    public static class ServicesBindings
    {
        public static IServiceCollection AddInfrastructureServices( 
            this IServiceCollection services )
        {
            return services.AddScoped<IUserService, UserService>();
        }
    }
}
