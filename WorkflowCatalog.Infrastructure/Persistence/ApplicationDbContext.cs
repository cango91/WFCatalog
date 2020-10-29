using System;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;
using static WorkflowCatalog.Application.Common.Interfaces.IDateTimeService;
using Microsoft.Extensions.Options;
using WorkflowCatalog.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using WorkflowCatalog.Domain.Common;
using System.Linq;
using System.Reflection;

namespace WorkflowCatalog.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _domainEventService = domainEventService;
        }

        public DbSet<Setup> Setups { get; set; }

        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<WorkflowDiagram> Diagrams { get; set; }
        public DbSet<UseCaseActor> Actors { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }
            int result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvents(cancellationToken);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents(CancellationToken cancellationToken)
        {
            var domainEventEntites = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .ToArray();
            foreach(var domainEvent in domainEventEntites)
            {
                await _domainEventService.Publish(domainEvent);
            }
        }
    }
}
