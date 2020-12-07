﻿using MediatR;
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

namespace WorkflowCatalog.Application.UseCases.Commands.UpdateUseCaseDetails
{
    public class UpdateUseCaseDetailsCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Preconditions { get; set; }
        public string Postconditions { get; set; }
        public List<Guid> Actors { get; set; }
        public string NormalCourse { get; set; }
        public string AltCourse { get; set; }
    }

    public class UpdateUseCaseDetailsCommandHandler : IRequestHandler<UpdateUseCaseDetailsCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUseCaseDetailsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateUseCaseDetailsCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.UseCases
                .Include(x=> x.UseCaseActors)
                .Where(x => x.Id == command.Id)
                .SingleOrDefaultAsync();
            if(entity == null)
            {
                throw new NotFoundException(nameof(UseCase), command.Id);
            }
            var actors = await _context.Actors
                .Where(x => command.Actors.Contains(x.Id))
                .ToListAsync(cancellationToken);

            entity.Name = command.Name;
            entity.Description = command.Description;
            entity.Preconditions = command.Preconditions;
            entity.Postconditions = command.Postconditions;
            entity.NormalCourse = command.NormalCourse;
            entity.AltCourse = command.AltCourse;
            entity.UseCaseActors = actors.Select(a => new UseCaseActor
            {
                ActorId = a.Id
            }).ToList();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
