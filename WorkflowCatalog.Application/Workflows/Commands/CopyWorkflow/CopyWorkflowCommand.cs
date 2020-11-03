using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Commands.CopyWorkflow
{
    public class CopyWorkflowCommand : IRequest<int>
    {
        public int WorkflowId { get; set; }
        public int SetupId { get; set; }
    }

    public class CopyWorkflowCommandHandler: IRequestHandler<CopyWorkflowCommand,int>
    {
        private readonly IApplicationDbContext _context;
        public CopyWorkflowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CopyWorkflowCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Workflows
                .AsNoTracking()
                .Include(x=> x.UseCases)
                .ThenInclude(x => x.Actors)
                .Include(x => x.Diagrams)
                .Where(x => x.Id == command.WorkflowId)
                .FirstOrDefaultAsync();

            if(entity==null)
            {
                throw new NotFoundException(nameof(Workflow), command.WorkflowId);
            }

            var setup = await _context.Setups
                .AsTracking()
                .Where(x => x.Id == command.SetupId)
                .FirstOrDefaultAsync();

            if (setup == null)
            {
                throw new NotFoundException(nameof(Setup), command.SetupId);
            }


            entity.Id = 0;
            setup.Workflows.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;

            
        }
    }
}
