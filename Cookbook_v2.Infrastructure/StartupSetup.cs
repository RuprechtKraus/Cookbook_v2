using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cookbook_v2.Infrastructure.Data;
using Cookbook_v2.Infrastructure.UoW;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Infrastructure.Data.Repositories;
using Cookbook_v2.Domain.UoW.Interfaces;

namespace Cookbook_v2.Infrastructure
{
    public static class StartupSetup
    {
        public static IServiceCollection AddDefaultInfrastructureDependencies(
            this IServiceCollection services,
            string connectionString,
            string migrationAssembly )
        {
            return services.AddDbContext<CookbookContext>( connectionString, migrationAssembly )
                .AddRepositories()
                .AddUnitOfWork();
        }

        private static IServiceCollection AddDbContext<T>(
            this IServiceCollection services,
            string connectionString,
            string migrationAssembly ) where T : DbContext
        {
            services.AddDbContext<T>(x => x.UseSqlServer(
                        connectionString, c => c.MigrationsAssembly( migrationAssembly ) ) );
            return services;
        }

        private static IServiceCollection AddRepositories( this IServiceCollection services )
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            return services;
        }

        private static IServiceCollection AddUnitOfWork( this IServiceCollection services )
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
