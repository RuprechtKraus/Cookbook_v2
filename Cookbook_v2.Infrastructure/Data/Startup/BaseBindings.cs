using Microsoft.Extensions.DependencyInjection;
using Cookbook_v2.Infrastructure.UoW;
using Cookbook_v2.Infrastructure.Services;

namespace Cookbook_v2.Infrastructure.Data.Startup
{
    public static class BaseBindings
    {
        public static IServiceCollection AddBaseServices( this IServiceCollection services )
        {
            return services
                .AddRepositories()
                .AddUnitOfWork()
                .AddInfrastructureServices();
        }
    }
}
