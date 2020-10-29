using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.Workflows.Commands.UpdateWorkflowDetails
{
    public class UpdateWorkflowDetailsCommandValidator : AbstractValidator<UpdateWorkflowDetailsCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateWorkflowDetailsCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("WorkflowId can not be empty")
                .NotNull().WithMessage("WorkflowId is required");

            RuleFor(x => x.PrimaryDiagramId)
                .MustAsync(BeValidDiagramId);
        }

        public async Task<bool>  BeValidDiagramId(UpdateWorkflowDetailsCommand model,int id, CancellationToken cancellationToken)
        {
            return await _context.Diagrams.
                Where(x => x.Workflow.Id == model.Id)
                .AnyAsync(x => x.Id == id);
        }
    }
}
