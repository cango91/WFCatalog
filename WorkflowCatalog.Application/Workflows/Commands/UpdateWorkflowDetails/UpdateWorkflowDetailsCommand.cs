using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Workflows.Commands.UpdateWorkflowDetails
{
    public class UpdateWorkflowDetailsCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkflowType { get; set; }
        public Guid? PrimaryDiagramId { get; set; }
    }
    public class UpdateWorkflowDetailsCommandHandler : IRequestHandler<UpdateWorkflowDetailsCommand>
    {
        private IApplicationDbContext _context;
        public UpdateWorkflowDetailsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateWorkflowDetailsCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Workflows.FindAsync(command.Id);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Workflow), command.Id);
            }

            var diag = await _context.Diagrams.FindAsync(command.PrimaryDiagramId);

            entity.Name = command.Name;
            entity.Description = command.Description;
            entity.Type = (WorkflowType) command.WorkflowType;
            entity.Primary = diag;
           

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
