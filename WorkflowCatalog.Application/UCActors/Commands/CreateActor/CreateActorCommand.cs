using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UCActors.Commands.CreateActor
{
    public class CreateActorCommand : IRequest<Guid> 
    {
        public Guid SetupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateActorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateActorCommand command, CancellationToken cancellationToken)
        {
            var setup = await _context.Setups.FindAsync(command.SetupId);
            if(setup == null)
            {
                throw new NotFoundException(nameof(Setup), command.SetupId);
            }
            var entity = new Actor
            {
                Name = command.Name,
                Description = command.Description,
                Setup = setup
            };

            _context.Actors.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
