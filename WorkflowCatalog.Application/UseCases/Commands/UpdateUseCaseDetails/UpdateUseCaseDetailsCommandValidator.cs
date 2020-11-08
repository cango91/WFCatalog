using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.UseCases.Commands.UpdateUseCaseDetails
{
    class UpdateUseCaseDetailsCommandValidator : AbstractValidator<UpdateUseCaseDetailsCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUseCaseDetailsCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be blank.");
            RuleFor(x => x.Actors)
                .MustAsync(BeValidActorIds).WithMessage("All actor Ids must be valid");
            RuleFor(x => x.NormalCourse)
                .NotEmpty().WithMessage("Normal course can not be empty");
        }

        public async Task<bool> BeValidActorIds(UpdateUseCaseDetailsCommand command, List<Guid> actors, CancellationToken cancellationToken)
        {
            var uc = await _context.UseCases.FindAsync(command.Id);
            return actors.All(x => uc.Workflow.Setup.Actors.Select(a => a.Id).Contains(x));
        }
    }
}
