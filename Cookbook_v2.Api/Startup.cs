using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cookbook_v2.Infrastructure.Web.Middleware;
using Cookbook_v2.Infrastructure;
using Cookbook_v2.Application;
using Cookbook_v2.Toolkit.Extensions;
using Cookbook_v2.Api.Middleware;
using Cookbook_v2.Application.Authentication;

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
            services.ConfigureAuthenticationOptions( Configuration );
            services.AddAppliactionServices();
            ConfigureDatabase(services);
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
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            } );
        }

        public void ConfigureAuthenticationOptions( IServiceCollection services )
        {
            services.Configure<AuthenticationOptions>(
                Configuration.GetSection( "AuthenticationOptions" ) );
        }

        public void ConfigureDatabase( IServiceCollection services )
        {
            string connectionString = Configuration.GetConnectionString( "CookbookConnection" );
            string migrationAssembly = Configuration.GetMigrationAssembly( "CookbookMigrations" );
            services.AddDefaultInfrastructureDependencies( connectionString, migrationAssembly );
        }
    }
}
