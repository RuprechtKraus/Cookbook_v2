using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cookbook_v2.Infrastructure.Data;
using Cookbook_v2.Infrastructure.Data.Startup;
using Cookbook_v2.Infrastructure.Web.Middleware;
using Cookbook_v2.Toolkit.Extensions;

namespace Cookbook_v2.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddControllers();
            ConfigureInfrastructure( services );
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Error" );
            }

            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            } );
        }

        public void ConfigureInfrastructure( IServiceCollection services )
        {
            ConfigureDatabase( services );
            services.AddBaseServices();
        }

        public void ConfigureDatabase( IServiceCollection services )
        {
            string connectionString = Configuration.GetConnectionString( "CookbookConnection" );
            string migrationAssembly = Configuration.GetMigrationAssembly( "CookbookMigrations" );
            services.AddDbContext<CookbookContext>( connectionString, migrationAssembly );
        }
    }
}
