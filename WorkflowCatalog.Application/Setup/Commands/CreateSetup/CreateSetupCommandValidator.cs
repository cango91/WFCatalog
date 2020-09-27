using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.Setup.Commands.CreateSetup
{
    public class CreateSetupCommandValidator : AbstractValidator<CreateSetupCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateSetupCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Name)
                .MaximumLength(200).WithMessage("Name is required")
                .NotEmpty().WithMessage("Name must not exceed 200 characters");

            RuleFor(x => x.ShortName)
                .MaximumLength(6).WithMessage("Abbreviation can not exceed 6 characters.")
                .NotEmpty().WithMessage("Abbreviation is required")
                .MustAsync(BeUniqueAbbreviation);


        }
        public async Task<bool> BeUniqueAbbreviation(string abb, CancellationToken cancellationToken)
        {
            return await _context.Setups
                .AllAsync(a => a.ShortName != abb);
        }
    }

}
