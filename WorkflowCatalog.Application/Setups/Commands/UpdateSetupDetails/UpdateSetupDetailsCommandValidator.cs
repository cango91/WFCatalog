using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.Setups.Commands.UpdateSetupDetails
{
    public class UpdateSetupDetailsCommandValidator : AbstractValidator<UpdateSetupDetailsCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateSetupDetailsCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.ShortName)
                .MaximumLength(6).WithMessage("Abbreviation can not exceed 6 characters.")
                .NotEmpty().WithMessage("Abbreviation is required.")
                .MustAsync(BeUniqueAbbreviation).WithMessage("Setup abbreviation must be unique.");
            RuleFor(x => x.Name)
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .NotEmpty().WithMessage("Name can not be empty.");
        }

        public async Task<bool> BeUniqueAbbreviation(UpdateSetupDetailsCommand model, string abb,CancellationToken cancellationToken)
        {
            return await _context.Setups
                .Where(x => x.Id != model.Id)
                .AllAsync(x => !String.Equals(x.ShortName.ToLower(),abb.ToLower()));

        }

    }
}
