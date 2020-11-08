using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.UCActors.Commands.CreateActor
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateActorCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.SetupId)
                .NotEmpty().WithMessage("SetupId must be specified")
                .MustAsync(BeValidSetupId).WithMessage("Invalid setup Id");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Actor name must be specified")
                .MaximumLength(60).WithMessage("Actor name can not exceed 60 characters")
                .MustAsync(BeUniqueNameForSetup).WithMessage("Actor name must be unique within setup");
        }

        public async Task<bool> BeValidSetupId(Guid guid, CancellationToken cancellationToken)
        {
            return await _context.Setups.FirstOrDefaultAsync(a => a.Id == guid) != null;
        }

        public async Task<bool> BeUniqueNameForSetup(CreateActorCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.Actors
                .Where(x => x.Setup.Id == command.SetupId)
                .AnyAsync(a => a.Name.ToLower() == command.Name.ToLower(), cancellationToken);
        }
    }
}
