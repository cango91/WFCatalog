using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Commands.CreateUseCase
{
    public class CreateUseCaseCommand : IRequest<int>
    {
        public int WorkflowId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Actors { get; set; }
        public string Preconditions { get; set; }
        public string Postconditions { get; set; }
        public string NormalCourse { get; set; }
        public string AltCourse { get; set; }
    }

    public class CreateUseCaseCommandHandler : IRequestHandler<CreateUseCaseCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateUseCaseCommandHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUseCaseCommand command, CancellationToken cancellationToken)
        {
            var actors = await _context.Actors
                .Where(x => command.Actors.Contains(x.Id))
                .ToListAsync(cancellationToken);
            var wf = await _context.Workflows.FindAsync(command.WorkflowId);
            var entity = new UseCase
            {
                Name = command.Name,
                Description = command.Description,
                Preconditions = command.Preconditions,
                Postconditions = command.Postconditions,
                NormalCourse = command.NormalCourse,
                AltCourse = command.AltCourse,
                Workflow = wf,
                Actors = actors
            };
            _context.UseCases.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
