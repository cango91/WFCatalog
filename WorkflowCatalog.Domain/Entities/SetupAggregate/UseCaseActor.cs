using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities.SetupAggregate
{
    public class UseCaseActor : AuditableEntity
    {
        public string Name { get; private set; }
        public UseCaseActor(string name)
        {
            Name = name;
        }

    }
}
