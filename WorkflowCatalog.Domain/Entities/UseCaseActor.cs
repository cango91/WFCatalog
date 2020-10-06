using System.Collections.Generic;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities
{
    public class UseCaseActor : AuditableEntity
    {
        public string Name { get; set; }
        public HashSet<UseCase> UseCases { get; set; }
    }
}
