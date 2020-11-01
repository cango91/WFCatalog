using System;
using FluentValidation;

namespace WorkflowCatalog.Application.Setups.Queries.DumpSetups
{
    public class DumpSetupsQueryValidator : AbstractValidator<DumpSetupsQuery>
    {
        public DumpSetupsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize must be at least 1");
        }
    }
}
