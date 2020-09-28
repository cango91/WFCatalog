using System.Collections.Generic;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Domain.Entities
{
    public class Workflow : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkflowType Type { get; set; }
        public List<UseCase> UseCases { get; set; }
        public List<WorkflowDiagram> Diagrams { get; set; }
        public Setup Setup { get; set; }

        public WorkflowDiagram Primary {get; set;}
    }
}
