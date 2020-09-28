using System;
using FluentValidation;
using WorkflowCatalog.Application.Common.Interfaces;

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
                .NotEmpty().WithMessage("SetupId can not be empty.");

        }
    }
}
