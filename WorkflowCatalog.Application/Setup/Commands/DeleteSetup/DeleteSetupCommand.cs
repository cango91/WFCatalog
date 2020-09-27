using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.Setup.Commands.DeleteSetup
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
            var entity = await _context.Setups.FindAsync(request.Id);
            if (entity==null)
            {
                throw new NotFoundException(nameof(WorkflowCatalog.Domain.Entities.Setup), request.Id);
            }

            _context.Setups.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
