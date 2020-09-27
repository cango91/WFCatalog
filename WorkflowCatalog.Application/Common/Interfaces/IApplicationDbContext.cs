using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Domain.Entities;


namespace WorkflowCatalog.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WorkflowCatalog.Domain.Entities.Setup> Setups { get; set; }
        DbSet<Workflow> Workflows { get; set; }
        DbSet<UseCase> UseCases { get; set; }
        DbSet<WorkflowDiagram> Diagrams { get; set; }
        DbSet<UseCaseActor> Actors { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
