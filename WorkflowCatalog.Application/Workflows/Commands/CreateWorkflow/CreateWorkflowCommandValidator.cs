using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Workflows.Commands.CreateWorkflow
{
    public class CreateWorkflowCommandValidator : AbstractValidator<CreateWorkflowCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateWorkflowCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Workflow name can not be empty.")
                .MaximumLength(200).WithMessage("Workflow name can cot exceed 200 characters.");

            RuleFor(x => x.SetupId)
                .NotEmpty().WithMessage("SetupId can not be empty.")
                .MustAsync(SetupIsActive).WithMessage("New Workflows can only be created for Setups with Active status.");


        }

        private async Task<bool> SetupIsActive(int id, CancellationToken cancellationToken)
        {
            var entity  = await _context.Setups.FindAsync(id);
            return entity.Status == SetupStatus.Active;
        }
    }
}
