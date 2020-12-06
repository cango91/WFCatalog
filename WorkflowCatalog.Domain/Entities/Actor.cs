using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities
{
    public class Actor : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UseCaseActor> UseCaseActors { get; set; }
        public Setup Setup { get; set; }
    }
}
