using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Diagrams.Commands.DeleteDiagram
{
    public class DeleteDiagramCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteDiagramCommandHandler : IRequestHandler<DeleteDiagramCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteDiagramCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteDiagramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagrams.FindAsync(command.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(WorkflowDiagram), command.Id);
            }

            _context.Diagrams.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
