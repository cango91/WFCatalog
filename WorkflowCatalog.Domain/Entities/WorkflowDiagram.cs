using System;
using System.Collections.Generic;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.ValueObjects;

namespace WorkflowCatalog.Domain.Entities
{
    public class WorkflowDiagram : AuditableEntity, IHasDomainEvent
    {
        public Filename Name { get; set; }
        public string ContentType { get;  set; }
        public Workflow Workflow { get; set; }
        public Byte[] File { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
