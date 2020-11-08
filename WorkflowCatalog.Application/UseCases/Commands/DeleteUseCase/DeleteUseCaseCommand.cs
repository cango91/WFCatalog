using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Commands.DeleteUseCase
{
    public class DeleteUseCaseCommand : IRequest
    {
        public Guid Id { get; set; }
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
            var entity = await _context.UseCases.FindAsync(command.Id);
            if(entity == null)
            {
                throw new NotFoundException(nameof(UseCase), command.Id);
            }
            _context.UseCases.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
