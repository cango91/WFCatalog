using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Infrastructure.Identity;

namespace WorkflowCatalog.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "cango", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "91");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if(!context.Setups.Any())
            {
                context.Setups.Add(new Setup
                {
                    Name = "Netherlands",
                    ShortName = "NL"
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
