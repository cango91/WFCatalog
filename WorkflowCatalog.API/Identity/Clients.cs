using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace WorkflowCatalog.API.Identity
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "oauthApiClient",
                    ClientName = "ApiClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>{new Secret("WFCatalogSecretKey".Sha256()) },
                    AllowedScopes = new List<String> {"api.read","api.write","api.admin"}
                }
            };
        }
    }
}
