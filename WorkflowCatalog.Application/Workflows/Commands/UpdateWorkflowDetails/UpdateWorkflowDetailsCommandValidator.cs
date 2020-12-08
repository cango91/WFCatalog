using FluentValidation;
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
                .MustAsync(BeUniqueNameForSetup).WithMessage("Workflow name must be unique for setup");
        }

        public async Task<bool> BeUniqueNameForSetup(UpdateWorkflowDetailsCommand command, string name, CancellationToken cancellationToken)
        {
            var wf = await _context.Workflows
                .Include(s => s.Setup)
                .FirstOrDefaultAsync( x=> x.Id == command.Id);
            if (wf == null)
            {
                throw new NotFoundException(nameof(Workflow), command.Id);
            }

            var entities = _context.Workflows.Where(x => (x.Setup.Id == wf.Setup.Id) && x.Id != command.Id);
            return !await entities
                .AnyAsync(a => a.Name.ToLower() == command.Name.ToLower(), cancellationToken);
        }

    }
}
