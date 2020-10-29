using System;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Domain.Events.SetupEvents
{
    public class SetupCreatedEvent : DomainEvent
    {
        public SetupCreatedEvent(Setup setup)
        {
            Setup = setup;
        }
        public Setup Setup {get;}
    }
}
