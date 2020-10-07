using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UCActors.Commands.AddActor
{
    public class AddActorCommand : IRequest<UCActorDto> 
    {
        public string Name { get; set; }
    }

    public class AddActorCommandHandler : IRequestHandler<AddActorCommand, UCActorDto>
    {
        private readonly IApplicationDbContext _context;

        public AddActorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UCActorDto> Handle(AddActorCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Actors
                .Where(x => x.Name.ToLower() == command.Name.ToLower())
                .FirstOrDefaultAsync(cancellationToken);
            if(entity!=null)
            {
                return new UCActorDto { Id = entity.Id, Name = entity.Name };
            }

            var uc = new UseCaseActor { Name = command.Name };
            _context.Actors.Add(uc);
            await _context.SaveChangesAsync(cancellationToken);
            return new UCActorDto { Id = uc.Id, Name = uc.Name };

        }
    }
}
