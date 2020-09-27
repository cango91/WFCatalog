using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities
{
    public class UseCase : AuditableEntity
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public List<UseCaseActor> Actors { get; set; }
        public string Preconditions { get;  set; }
        public string Postconditions { get;  set; }
        public string NormalCourse { get;  set; }
        public string AltCourse { get;  set; }
        public Workflow Workflow { get; set; }

      

    }
}
