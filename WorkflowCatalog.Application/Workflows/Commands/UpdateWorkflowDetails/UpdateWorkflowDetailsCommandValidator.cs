using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                .NotEmpty().WithMessage("Id can not be blank");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be blank")
                .MustAsync(BeUniqueNameForSetup);
        }

        public async Task<bool> BeUniqueNameForSetup(UpdateWorkflowDetailsCommand command, string name, CancellationToken cancellationToken)
        {
            var wf = await _context.Workflows.FindAsync(command.Id);
            return !await _context.Workflows
                .Where(x => (x.Setup.Id == wf.Setup.Id) && x.Id != command.Id)
                .AnyAsync(a => a.Name.ToLower() == command.Name.ToLower(), cancellationToken);
        }

    }
}
