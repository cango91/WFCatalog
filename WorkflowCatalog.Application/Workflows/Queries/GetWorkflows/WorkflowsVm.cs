using System;
using System.Collections.Generic;
using WorkflowCatalog.Application.Common.Models;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflows
{
    public class WorkflowsVm
    {
        public IList<WorkflowTypeDto> WorkflowTypes { get; set; }
        public PaginatedList<WorkflowsDto> Workflows { get; set; }
    }
}
