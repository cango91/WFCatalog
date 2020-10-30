using System;
using FluentValidation;

namespace WorkflowCatalog.Application.UseCases.Queries.GetUseCases
{
    public class GetUseCasesQueryValidator : AbstractValidator<GetUseCasesQuery>
    {
        public GetUseCasesQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize must be at least 1");
        }
    }
}
