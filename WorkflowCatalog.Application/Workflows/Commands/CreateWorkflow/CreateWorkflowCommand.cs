using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Workflows.Commands.CreateWorkflow
{
    public class CreateWorkflowCommand : IRequest<Guid>
    {
        public Guid SetupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkflowType { get; set; }
    }

    public class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand,Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateWorkflowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateWorkflowCommand command, CancellationToken cancellationToken)
        {
            var setup = await _context.Setups.FindAsync(command.SetupId);
            if(setup == null)
            {
                throw new NotFoundException(nameof(Setup), command.SetupId);
            }
            var entity = new Workflow
            {
                Name = command.Name,
                Description = command.Description,
                Type = (WorkflowType)command.WorkflowType,
                Setup = setup
            };
            _context.Workflows.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

    }
}
