using System;
using FluentValidation;

namespace WorkflowCatalog.Application.Diagrams.Queries.DumpDiagramsInfo
{
    public class DumpDiagramsInfoQueryValidator : AbstractValidator<DumpDiagramsInfoQuery>
    {
        public DumpDiagramsInfoQueryValidator()
        {
            RuleFor(x => x.Page)
             .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize must be at least 1");
        }
    }
}
