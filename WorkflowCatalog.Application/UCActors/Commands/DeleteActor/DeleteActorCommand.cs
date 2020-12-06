using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UCActors.Commands.DeleteActor
{
    public class DeleteActorCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteActorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteActorCommand command,CancellationToken cancellationToken)
        {
            var entity = await _context.Actors
                .Where(x => x.Id == command.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Actor), command.Id);
            }

            _context.Actors.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
