using System;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Domain.Events.UseCaseEvents
{
    public class UseCaseRemovedEvent : DomainEvent
    {
        public UseCaseRemovedEvent(UseCase useCase)
        {
            UseCase = useCase;
        }

        public UseCase UseCase { get; }
    }
}
