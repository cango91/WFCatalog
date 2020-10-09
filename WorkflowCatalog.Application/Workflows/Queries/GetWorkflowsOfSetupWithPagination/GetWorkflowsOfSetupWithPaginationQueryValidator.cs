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
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize must be at least 1");

            RuleFor(x => x.SortBy)
                .Must(BeValidSortBy);

            RuleFor(x => x.FilterTypes)
                .Must(BeValidFilterType);

            
  
        }
        public bool BeValidSortBy(string sortBy)
        {
            var validSorts = new List<string> { "name", "description", "type","id" };
            return validSorts.Exists(x => x == sortBy.ToLowerInvariant());
        }

        public bool BeValidSortOrder(string sortOrder)
        {
            var validSortOrders = new List<string> { "asc", "ascending", "desc", "descending" };
            return validSortOrders.Exists(x => x == sortOrder.ToLowerInvariant());
        }

        public bool BeValidFilterType(List<int> filters)
        {
            var validFilters = new List<int> { (int) WorkflowType.MainFlow, (int) WorkflowType.SubFlow};
            return filters.All(x => validFilters.Contains(x));
        }
    }
}
