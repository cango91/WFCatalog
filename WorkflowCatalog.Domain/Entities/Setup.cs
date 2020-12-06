using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Enums;
using WorkflowCatalog.Domain.Events.SetupEvents;
using WorkflowCatalog.Domain.Exceptions;

namespace WorkflowCatalog.Domain.Entities
{
    public class Setup : AuditableEntity, IHasDomainEvent, IAggregateRoot
    {
        public string Name { get;  set; }
        public string ShortName { get;  set; }
        public string Description { get;  set; }

        private SetupStatus _status;
        public SetupStatus Status
        {
            get => _status;
            set
            {
                if(value!=_status)
                {
                    DomainEvents.Add(new SetupStatusChangedEvent(this));
                }
                _status = value;
            }
        }

        public HashSet<Workflow> Workflows { get; set; } = new HashSet<Workflow>();
        public HashSet<Actor> Actors { get; set; } = new HashSet<Actor>();

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();


    }
}
