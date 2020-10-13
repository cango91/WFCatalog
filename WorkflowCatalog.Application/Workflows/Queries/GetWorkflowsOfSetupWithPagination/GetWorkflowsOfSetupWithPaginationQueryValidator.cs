using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowsOfSetupWithPagination
{
    public class GetWorkflowsOfSetupWithPaginationQueryValidator : AbstractValidator<GetWorkflowsOfSetupWithPaginationQuery>
    {
        public GetWorkflowsOfSetupWithPaginationQueryValidator()
        {
            RuleFor(x => x.SetupId)
                .NotNull()
                .NotEmpty().WithMessage("SetupId is required.");
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize must be at least 1");

            
  
        }
        
    }
}
