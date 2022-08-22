using Microsoft.Extensions.DependencyInjection;
using Cookbook_v2.Infrastructure.Data.UoW;
using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Infrastructure.UoW
{
    public static class UnitOfWorkBindings
    {
        public static IServiceCollection AddUnitOfWork( this IServiceCollection services )
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
