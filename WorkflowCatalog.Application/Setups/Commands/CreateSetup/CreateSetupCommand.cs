using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Enums;
using WorkflowCatalog.Domain.Events.SetupEvents;

namespace WorkflowCatalog.Application.Setups.Commands.CreateSetup
{
    public class CreateSetupCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }

    public class CreateSetupCommandHandler : IRequestHandler<CreateSetupCommand,Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateSetupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateSetupCommand request,CancellationToken cancellationToken)
        {
            var entity = new Setup
            {
                Name = request.Name,
                ShortName = request.ShortName,
                Status = SetupStatus.Active,
                Description = request.Description,
                Workflows = new HashSet<Workflow>()

            };

            entity.DomainEvents.Add(new SetupCreatedEvent(entity));

            _context.Setups.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
