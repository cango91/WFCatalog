using System;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
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
