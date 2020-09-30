using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace WorkflowCatalog.API.Identity
{
    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<String> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "api",
                    DisplayName = "API",
                    Description = "Allow the application to access WorkflowCatalogAPI on your behalf",
                    Scopes = new List<String> {"api.read","api.write","api.admin"},
                    ApiSecrets = new List<Secret> {new Secret("ScopesSecret".Sha256())},
                    UserClaims = new List<String> {"role"}
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("api.read","Read access to WorkflowCatalogAPI"),
                new ApiScope("api.write","Write access to WorkflowCatalogAPI"),
                new ApiScope("api.admin","Administrative access to WorkflowCatalogAPI")
            };
        }
    }
}
