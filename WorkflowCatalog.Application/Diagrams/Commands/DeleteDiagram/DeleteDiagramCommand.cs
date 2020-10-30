using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Events.WorkflowDiagramEvents;

namespace WorkflowCatalog.Application.Diagrams.Commands.DeleteDiagram
{
    public class DeleteDiagramCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteDiagramCommandHandler  : IRequestHandler<DeleteDiagramCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteDiagramCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit>  Handle (DeleteDiagramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagrams
                .Where(x => x.Id == command.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity==null)
            {
                throw new NotFoundException(nameof(WorkflowDiagram), command.Id);
            }

            entity.DomainEvents.Add(new DiagramRemovedEvent(entity));

            _context.Diagrams.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
