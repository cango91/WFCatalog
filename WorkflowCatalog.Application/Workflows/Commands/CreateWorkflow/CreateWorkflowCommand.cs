using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Enums;
using WorkflowCatalog.Domain.Entities;


namespace WorkflowCatalog.Application.Workflows.Commands.CreateWorkflow
{
    public class CreateWorkflowCommand : IRequest<int>
    {
        public int SetupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkflowType Type { get; set; }
    }

    public class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateWorkflowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateWorkflowCommand request,CancellationToken cancellationToken)
        {
            var entity = await _context.Setups.FindAsync(request.SetupId);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Setup),request.SetupId);
            }

            var wf = new Workflow
            {
                Name = request.Name,
                Description = request.Description,
                Type = request.Type,
                Setup = entity
            };

            _context.Workflows.Add(wf);

            await _context.SaveChangesAsync(cancellationToken);

            return wf.Id;

        }
    }
}