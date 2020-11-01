using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Commands.UpdateUseCaseDetails
{
    public class UpdateUseCaseDetailsCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Actors { get; set; }
        public string Preconditions { get; set; }
        public string Postconditions { get; set; }
        public string NormalCourse { get; set; }
        public string AltCourse { get; set; }
    }

    public class UpdateUseCaseDetailsCommandHandler : IRequestHandler<UpdateUseCaseDetailsCommand>
    {
        private IApplicationDbContext _context;
        public UpdateUseCaseDetailsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUseCaseDetailsCommand command, CancellationToken cancellationToken)
        {
            var entity = _context.UseCases.Find(command.Id);
            var actors = await _context.Actors
                .Where(x => command.Actors.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(UseCase), command.Id);
            }

            entity.Name = command.Name;
            entity.Description = command.Description;
            entity.Actors = actors;
            entity.Preconditions = command.Preconditions;
            entity.Postconditions = command.Postconditions;
            entity.NormalCourse = command.NormalCourse;
            entity.AltCourse = command.AltCourse;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
