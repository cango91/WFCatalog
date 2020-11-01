using System;
using System.Collections.Generic;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflows;

namespace WorkflowCatalog.Application.Workflows.Queries.DumpWorkflows
{
    public class WorkflowsDumpVm
    {
        public IList<WorkflowTypeDto> WorkflowTypes { get; set; }
        public PaginatedList<WorkflowsDumpDto> Workflows { get; set; }
    }
}
