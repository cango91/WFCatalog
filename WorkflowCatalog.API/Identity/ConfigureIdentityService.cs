using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
//using Microsoft.Extensions.

namespace WorkflowCatalog.API.Identity
{
    public static class ConfigureIdentityService
    {
        public static void AddAndConfigureIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddInMemoryApiScopes(Resources.GetApiScopes())
                .AddTestUsers(TestUsers.Get())
                .AddDeveloperSigningCredential();

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "WorkflowCatalog API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // base-address of your identityserver
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("asd")),
                        ValidateIssuer = false
                    };
                })
                .AddOpenIdConnect(o =>
                {
                    o.ClientId = "oauthApiClient";
                    o.ResponseType = "id_token token";
                    o.Authority = "https://localhost:5001";
                    o.Scope.Add("api.read");
                    o.Scope.Add("api.write");
                    o.Scope.Add("api.admin");

                });
        }
    }
}
