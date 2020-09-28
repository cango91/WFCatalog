using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Entities;
namespace WorkflowCatalog.Domain.Events.SetupEvents
{
    public class SetupStatusChangedEvent : DomainEvent
    {
        public SetupStatusChangedEvent(Setup setup)
        {
            Setup = setup;
        }

        public Setup Setup { get; }
    }
}
