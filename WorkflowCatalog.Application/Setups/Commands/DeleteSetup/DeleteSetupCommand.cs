using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Domain.Events.SetupEvents;

namespace WorkflowCatalog.Application.Setups.Commands.DeleteSetup
{
    public class DeleteSetupCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteSetupCommandHandler : IRequestHandler<DeleteSetupCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSetupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle (DeleteSetupCommand request,CancellationToken cancellationToken)
        {
            var entity = await _context.Setups
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity==null)
            {
                throw new NotFoundException(nameof(WorkflowCatalog.Domain.Entities.Setup), request.Id);
            }
            entity.DomainEvents.Add(new SetupRemovedEvent(entity));

            _context.Setups.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
