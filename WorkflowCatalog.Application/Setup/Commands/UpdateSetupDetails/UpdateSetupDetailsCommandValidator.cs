using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.Setup.Commands.UpdateSetupDetails
{
    public class UpdateSetupDetailsCommandValidator : AbstractValidator<UpdateSetupDetailsCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateSetupDetailsCommandValidator(IApplicationDbContext context)
        {
            _context = context;


        }

        public async Task<bool> BeUniqueAbbreviation(UpdateSetupDetailsCommand model, string abb,CancellationToken cancellationToken)
        {
            return await _context.Setups
                .Where(x => x.Id != model.Id)
                .AllAsync(x => !String.Equals(x.ShortName.ToLowerInvariant(),abb.ToLowerInvariant()));

        }

    }
}
