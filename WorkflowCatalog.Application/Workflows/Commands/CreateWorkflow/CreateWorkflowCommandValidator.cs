using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            RuleFor(x => x.SetupId)
                .NotEmpty().WithMessage("SetupId can not be blank")
                .MustAsync(BeValidSetupId).WithMessage("Invalid SetupId");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be blank")
                .MustAsync(BeUniqueNameForSetup).WithMessage("Workflow name must be unique within setup.");

            RuleFor(x => x.WorkflowType)
                .Must(BeValidWorkflowType).WithMessage("Invalid Workflow Type");
        }

        public async Task<bool> BeUniqueNameForSetup(CreateWorkflowCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.Workflows
                .Where(x => x.Setup.Id == command.SetupId)
                .AnyAsync(a => a.Name.ToLower() == command.Name.ToLower(), cancellationToken);
        }

        public bool BeValidWorkflowType(int type)
        {
            try
            {
                var _ = (WorkflowType) type;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> BeValidSetupId(Guid guid, CancellationToken cancellationToken)
        {
            return await _context.Setups.FirstOrDefaultAsync(a => a.Id == guid) != null;
        }
    }
}
