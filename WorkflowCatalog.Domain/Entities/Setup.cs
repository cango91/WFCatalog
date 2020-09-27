using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Enums;
using WorkflowCatalog.Domain.Exceptions;

namespace WorkflowCatalog.Domain.Entities
{
    public class Setup : AuditableEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public SetupStatus Status { get; set; }
        //public SetupStatus ToggleStatus => Status = !Status;
        public HashSet<Workflow> Workflows { get; set; }
    }
}
