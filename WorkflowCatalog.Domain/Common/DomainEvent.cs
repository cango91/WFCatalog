using System;
using System.Collections.Generic;

namespace WorkflowCatalog.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }


    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccured = DateTimeOffset.UtcNow;
        }
        public DateTimeOffset DateOccured { get; protected set; } = DateTime.UtcNow;
    }
}
