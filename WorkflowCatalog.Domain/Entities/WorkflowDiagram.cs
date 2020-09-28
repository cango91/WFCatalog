using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities
{
    public class WorkflowDiagram : AuditableEntity
    {
        public string Name { get;  set; }
        public string MimeType { get;  set; }
        public Workflow Workflow { get; set; }
        public Byte[] File { get; set; }
    }
}
