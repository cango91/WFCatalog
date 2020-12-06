using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Commands.CreateUseCase
{
    public class CreateUseCaseCommand : IRequest<Guid>
    {
        public Guid WorkflowId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> Actors { get; set; }
        public string Preconditions { get; set; }
        public string Postconditions { get; set; }
        public string NormalCourse { get; set; }
        public string AltCourse { get; set; }
    }

    public class CreateUseCaseCommandHandler : IRequestHandler<CreateUseCaseCommand,Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateUseCaseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUseCaseCommand command, CancellationToken cancellationToken)
        {
            var actors = await _context.Actors
                .Where(x => command.Actors.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var workflow = await _context.Workflows.FindAsync(command.WorkflowId);

            var entity = new UseCase
            {
                Name = command.Name,
                Description = command.Description,
                UseCaseActors = actors.Select(a => new UseCaseActor
                {
                    ActorId = a.Id
                }).ToList(),
                Preconditions = command.Preconditions,
                Postconditions = command.Postconditions,
                NormalCourse = command.NormalCourse,
                AltCourse = command.AltCourse,
                Workflow = workflow
            };

            _context.UseCases.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;

        }
    }
}
