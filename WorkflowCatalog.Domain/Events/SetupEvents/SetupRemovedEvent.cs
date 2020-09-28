using System;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Domain.Events.SetupEvents
{
    public class SetupRemovedEvent : DomainEvent
    {
        public SetupRemovedEvent(Setup setup)
        {
            Setup = setup;
        }
        public Setup Setup { get; }
    }
}
