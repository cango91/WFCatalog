using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Events.WorkflowDiagramEvents;

namespace WorkflowCatalog.Application.Diagrams.Commands.CreateDiagram
{
    public class CreateDiagramCommand  : IRequest<int>
    {
        public int WorkflowId { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] File { get; set; }
    }

    public class CreateDiagramCommandHandler : IRequestHandler<CreateDiagramCommand,int>
    {
        private readonly IApplicationDbContext _context;
        public CreateDiagramCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle (CreateDiagramCommand command, CancellationToken cancellationToken)
        {
            var wf = await _context.Workflows.FindAsync(command.WorkflowId);
            var entity = new WorkflowDiagram
            {
                Name = command.Name,
                MimeType = command.MimeType,
                File = command.File
            };
            wf.Diagrams.Add(entity);
            entity.DomainEvents.Add(new DiagramCreatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
