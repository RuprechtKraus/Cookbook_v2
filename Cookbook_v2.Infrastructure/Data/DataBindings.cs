using Microsoft.Extensions.DependencyInjection;
using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Domain.RecipeModel;
using Cookbook_v2.Domain.CategoryModel;
using Cookbook_v2.Infrastructure.Data.UserModel;
using Cookbook_v2.Infrastructure.Data.RecipeModel;
using Cookbook_v2.Infrastructure.Data.CategoryModel;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_v2.Infrastructure.Data
{
    public static class DataBindings
    {
        public static IServiceCollection AddRepositories( this IServiceCollection services )
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }

        public static IServiceCollection AddDbContext<T>(
            this IServiceCollection services,
            string connectionString,
            string migrationAssembly ) where T : DbContext
        {
            return services
                .AddDbContext<T>(
                    x => x.UseSqlServer(
                        connectionString,
                        c => c.MigrationsAssembly( migrationAssembly ) ) );
        }
    }
}
