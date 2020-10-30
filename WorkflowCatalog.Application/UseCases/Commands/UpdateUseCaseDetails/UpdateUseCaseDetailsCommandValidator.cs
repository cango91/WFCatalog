using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.UseCases.Commands.UpdateUseCaseDetails
{
    public class UpdateUseCaseDetailsCommandValidator : AbstractValidator<UpdateUseCaseDetailsCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUseCaseDetailsCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            _context = context;
            RuleFor(v => v.Name)
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .NotEmpty().WithMessage("Name is required.");
            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Description must not be empty.");
            RuleFor(v => v.NormalCourse)
                .NotEmpty().WithMessage("Normal course of use case can not be empty.");
            RuleFor(v => v.Actors)
                .MustAsync(MustExistAllActors);
        }

        public async Task<bool> MustExistAllActors(IEnumerable<int> actors, CancellationToken cancellationToken)
        {
            var validIds = await _context.Actors.Select(x => x.Id).ToListAsync();
            return actors.All(x => validIds.Contains(x));
        }
    }
}
