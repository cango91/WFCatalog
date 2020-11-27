using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCatalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;
using static WorkflowCatalog.Application.Common.Interfaces.IDateTimeService;
using WorkflowCatalog.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace WorkflowCatalog.Infrastructure
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")?? "Server=10.5.0.110;Port=5432;Database=workflow;User Id=WORKFLOW;Password=001992;",
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();


            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = false,
                       ValidateIssuerSigningKey = false,
                       ValidIssuer = configuration["Token:Issuer"],
                       ValidAudience = configuration["Token:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"]))
                   };
               });

            return services;
        }
    }

   
}
