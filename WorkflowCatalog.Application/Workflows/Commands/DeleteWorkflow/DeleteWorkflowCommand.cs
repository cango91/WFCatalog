using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Commands.DeleteWorkflow
{
    public class DeleteWorkflowCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteWorkflowCommandHandler : IRequestHandler<DeleteWorkflowCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteWorkflowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle (DeleteWorkflowCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Workflows.FindAsync(command.Id);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Workflow), command.Id);
            }

            _context.Workflows.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
