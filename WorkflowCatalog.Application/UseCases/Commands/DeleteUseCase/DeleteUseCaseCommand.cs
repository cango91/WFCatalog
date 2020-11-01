using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Events.UseCaseEvents;

namespace WorkflowCatalog.Application.UseCases.Commands.DeleteUseCase
{
    public class DeleteUseCaseCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteUseCaseCommandHandler : IRequestHandler<DeleteUseCaseCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUseCaseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUseCaseCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.UseCases
                .Where(x => x.Id == command.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if(entity==null)
            {
                throw new NotFoundException(nameof(UseCase), command.Id);
            }

            entity.DomainEvents.Add(new UseCaseRemovedEvent(entity));

            _context.UseCases.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
