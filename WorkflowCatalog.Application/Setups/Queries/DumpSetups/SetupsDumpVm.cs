using System;
using System.Collections.Generic;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Setups.Queries.GetSetups;

namespace WorkflowCatalog.Application.Setups.Queries.DumpSetups
{
    public class SetupsDumpVm
    {
        public IList<SetupStatusDto> SetupStatuses { get; set; }
        public PaginatedList<SetupsDumpDto> Setups { get; set; }
    }
}
