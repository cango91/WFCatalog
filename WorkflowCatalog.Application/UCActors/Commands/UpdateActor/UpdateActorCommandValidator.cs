using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.UCActors.Commands.UpdateActor
{
    class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateActorCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Actor name must be specified")
                .MaximumLength(60).WithMessage("Actor name can not exceed 60 characters")
                .MustAsync(BeUniqueNameForSetup).WithMessage("Actor name must be unique within setup");
        }


        public async Task<bool> BeUniqueNameForSetup(UpdateActorCommand command, string name, CancellationToken cancellationToken)
        {
            var actor = await _context.Actors.FindAsync(command.Id);
            return !await _context.Actors
                .Where(x => (x.Setup.Id == actor.Setup.Id) && x.Id != actor.Id)
                .AnyAsync(a => a.Name.ToLower() == command.Name.ToLower(), cancellationToken);
        }
    }
}
