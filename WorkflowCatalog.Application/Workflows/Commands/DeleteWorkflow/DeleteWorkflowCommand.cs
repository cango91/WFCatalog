using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Commands.DeleteWorkflow
{
    public class DeleteWorkflowCommand : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteWorkflowCommandHandler : IRequestHandler<DeleteWorkflowCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteWorkflowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteWorkflowCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Workflows.FindAsync(request.Id);
            

            if(entity==null)
            {
                throw new NotFoundException(nameof(Workflow),request.Id);
            }

            _context.Workflows.Remove(entity);


            return Unit.Value;
        }
    }

}
