using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities
{
    public class WorkflowDiagram : ValueObject
    {

        public string Filename { get;  set; }
        public bool IsPrimary { get;  set; }
        public string FileType { get;  set; }
        public Workflow Workflow { get; set; }
        public Byte File { get; set; }

        public void TogglePrimary()
        {
            IsPrimary = !IsPrimary;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FileType;
            yield return Filename;
            yield return Workflow;
        }

         
       
    }
}
