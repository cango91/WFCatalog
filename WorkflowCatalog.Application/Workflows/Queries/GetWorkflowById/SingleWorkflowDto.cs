using System;
using System.Collections.Generic;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class SingleWorkflowDto : IMapFrom<Workflow>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public List<int> UseCases { get; set; }
        public List<int> Diagrams { get; set; }
        public int SetupId { get; set; }

        public int PrimaryDiagramId { get; set; }
    }

}
