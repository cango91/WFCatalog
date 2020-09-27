using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities.SetupAggregate
{
    public class WorkflowDiagram : ValueObject
    {
        private byte[] _diagram;
        private Workflow _owner;
        public string Filename { get; private set; }
        public bool IsPrimary { get; private set; }
        public string FileType { get; private set; }

        public WorkflowDiagram(Workflow owner, string filename, byte[] diagram, string filetype, bool isPrimary=false)
        {
            _owner = owner;
            _diagram = diagram;
            Filename = filename;
            IsPrimary = isPrimary;
            FileType = filetype;
        }

        public Workflow GetOwner => _owner;

        public void SetName(string name)
        {
            Filename = name;
        }

        public void SetPrimary(bool val)
        {
            IsPrimary = val;
        }
        public void TogglePrimary()
        {
            IsPrimary = !IsPrimary;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FileType;
            yield return Filename;
            yield return _owner;
        }

         
       
    }
}
