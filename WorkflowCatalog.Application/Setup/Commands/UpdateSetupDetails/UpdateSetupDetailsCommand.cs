using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Setup.Commands.UpdateSetupDetails
{
    public class UpdateSetupDetailsCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public SetupStatus Status { get; set; }
    }

    public class UpdateSetupDetailsCommandHandler : IRequestHandler<UpdateSetupDetailsCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateSetupDetailsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSetupDetailsCommand request,CancellationToken cancellationToken)
        {
            var entity = await _context.Setups.FindAsync(request.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(WorkflowCatalog.Domain.Entities.Setup),request.Id);
            }

            entity.Name = request.Name;
            entity.ShortName = request.ShortName;
            entity.Status = request.Status;

            return Unit.Value;

        }

    }
}
