using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities
{
    public class UseCaseActor : AuditableEntity
    {
        public string Name { get; set; }
    }
}
