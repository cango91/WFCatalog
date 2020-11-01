using System;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Domain.Events.WorkflowDiagramEvents
{
    public class DiagramCreatedEvent : DomainEvent
    {
        public DiagramCreatedEvent(WorkflowDiagram diagram)
        {
            Diagram = diagram;
        }
        public WorkflowDiagram Diagram { get; }
    }
}
