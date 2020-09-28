using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Enums;
using WorkflowCatalog.Domain.Events.SetupEvents;

namespace WorkflowCatalog.Application.Setups.Commands.CreateSetup
{
    public class CreateSetupCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }

    public class CreateSetupCommandHandler : IRequestHandler<CreateSetupCommand,int>
    {
        private readonly IApplicationDbContext _context;

        public CreateSetupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateSetupCommand request,CancellationToken cancellationToken)
        {
            var entity = new Setup
            {
                Name = request.Name,
                ShortName = request.ShortName,
                Status = SetupStatus.Active,
                Workflows = new HashSet<Workflow>()

            };

            entity.DomainEvents.Add(new SetupCreatedEvent(entity));

            _context.Setups.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
