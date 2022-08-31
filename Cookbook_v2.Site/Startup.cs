using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cookbook_v2.Site
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddSpaStaticFiles( configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseStaticFiles();

            if ( !env.IsDevelopment() )
            {
                app.UseSpaStaticFiles();
            }

            app.UseSpa( spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if ( env.IsDevelopment() )
                {
                    spa.UseAngularCliServer( npmScript: "start" );
                }
            } );
        }
    }
}
