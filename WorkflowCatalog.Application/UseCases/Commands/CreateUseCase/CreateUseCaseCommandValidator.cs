using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.UseCases.Commands.CreateUseCase
{
    public class CreateUseCaseCommandValidator : AbstractValidator<CreateUseCaseCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateUseCaseCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be blank.");

            RuleFor(x => x.WorkflowId)
                .NotEmpty().WithMessage("WorkflowId can not be empty")
                .MustAsync(BeValidWorkflowId).WithMessage("WorkflowId must be valid");
            RuleFor(x => x.Actors)
                .MustAsync(BeValidActorIds).WithMessage("All actor Ids must be valid");
            RuleFor(x => x.NormalCourse)
                .NotEmpty().WithMessage("Normal course can not be empty");
        }
        public async Task<bool> BeValidWorkflowId(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Workflows.FindAsync(id) != null;
        }

        public async Task<bool> BeValidActorIds (CreateUseCaseCommand command, List<Guid> actors, CancellationToken cancellationToken)
        {
            var wf = await _context.Workflows.FindAsync(command.WorkflowId);
            return actors.All(x => wf.Setup.Actors.Select(a => a.Id).Contains(x));
        }
    }
}
