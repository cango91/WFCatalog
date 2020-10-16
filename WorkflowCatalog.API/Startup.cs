using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowCatalog.API.Services;
using WorkflowCatalog.Application;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Infrastructure;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;
using WorkflowCatalog.API.Identity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Sieve.Services;
using Sieve.Models;

namespace WorkflowCatalog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            //services.AddControllers();
            
            
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddAndConfigureIdentityServer();

            services.AddLogging();

            services.AddScoped<SieveProcessor>();

            services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));

            



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            //app.UseOpenIdConnectAuthentication(a);

            app.UseAuthorization();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
