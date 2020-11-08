using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.ValueObjects;

namespace WorkflowCatalog.Application.Diagrams.Commands.CreateDiagram
{
    public class CreateDiagramCommand : IRequest<Guid>
    {
        public Guid WorkflowId { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] File { get; set; }
    }
    public class CreateDiagramCommandHandler : IRequestHandler<CreateDiagramCommand,Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateDiagramCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateDiagramCommand command, CancellationToken cancellationToken)
        {
            var wf = await _context.Workflows.FindAsync(command.WorkflowId);
            if(wf==null)
            {
                throw new NotFoundException(nameof(Workflow), command.WorkflowId);
            }
            var entity = new WorkflowDiagram
            {
                Name = (Filename) command.Name,
                ContentType = command.ContentType,
                File = command.File,
                Workflow = wf
            };
            wf.Diagrams.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
