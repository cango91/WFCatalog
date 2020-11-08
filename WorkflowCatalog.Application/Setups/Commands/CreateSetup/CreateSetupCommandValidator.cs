using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using System;

namespace WorkflowCatalog.Application.Setups.Commands.CreateSetup
{
    public class CreateSetupCommandValidator : AbstractValidator<CreateSetupCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateSetupCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Name)
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.ShortName)
                .MaximumLength(6).WithMessage("Abbreviation can not exceed 6 characters.")
                .NotEmpty().WithMessage("Abbreviation is required.")
                .MustAsync(BeUniqueAbbreviation).WithMessage("Setup abbreviation must be unique.") ;


        }
        public async Task<bool> BeUniqueAbbreviation(string abb, CancellationToken cancellationToken)
        {
            
            var val = await _context.Setups
                .AllAsync(a => !String.Equals(a.ShortName.ToLower(), abb.ToLowerInvariant()));
            return val;
        }
    }

}
