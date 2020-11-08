using System.Collections.Generic;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities
{
    public class UseCaseActor : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public HashSet<UseCase> UseCases { get; set; } = new HashSet<UseCase>();
        public Setup Setup { get; set; }
    }
}
