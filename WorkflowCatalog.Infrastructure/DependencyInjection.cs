using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCatalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Infrastructure.Identity;
using static WorkflowCatalog.Application.Common.Interfaces.IDateTimeService;
using Microsoft.AspNetCore.Authentication;
using WorkflowCatalog.Infrastructure.Services;

namespace WorkflowCatalog.Infrastructure
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")?? "Server=127.0.0.1;Port=5432;Database=WFCatalogDb;User Id=catalogUser;Password=p@ss;",
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddAuthentication()
                .AddIdentityServerJwt();
            return services;
        }
    }

   
}
