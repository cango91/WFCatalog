using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UCActors.Commands.UpdateActor
{
    public class UpdateActorCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateActorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle (UpdateActorCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Actors.FindAsync(command.Id);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Actor), command.Id);
            }

            entity.Name = command.Name;
            entity.Description = command.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
