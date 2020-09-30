using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace WorkflowCatalog.API.Identity
{
    internal class TestUsers
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser> {
            new TestUser {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "cango",
                Password = "91",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "can.gologlu@gmail.com"),
                    new Claim(JwtClaimTypes.Role, "admin")
                }
            }
        };
        }
    }
}
