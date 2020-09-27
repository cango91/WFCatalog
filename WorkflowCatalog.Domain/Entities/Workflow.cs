using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Enums;
using WorkflowCatalog.Domain.Exceptions;

namespace WorkflowCatalog.Domain.Entities
{
    public class Workflow : AuditableEntity
    {
        public string Name{ get; set;}
        public string Description { get; set; }
        public WorkflowType Type { get; set; }
        public List<UseCase> UseCases { get; set; }
        public List<WorkflowDiagram> Diagrams { get; set; }
        public Setup Setup { get; set; }


        
    }
}
