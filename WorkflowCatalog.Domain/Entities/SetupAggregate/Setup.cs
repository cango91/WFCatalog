using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Enums;
using WorkflowCatalog.Domain.Exceptions;

namespace WorkflowCatalog.Domain.Entities.SetupAggregate
{
    public class Setup : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        private HashSet<Workflow> _workflows;
        public IReadOnlyCollection<Workflow> Workflows => _workflows;

        private SetupStatus _setupStatus;
        public SetupStatus GetStatus => _setupStatus;
        public SetupStatus ToggleStatus => _setupStatus = !_setupStatus;

        protected Setup()
        {
            _workflows = new HashSet<Workflow>();
            _setupStatus = SetupStatus.Active;
        }
        public Setup(string name, string abbreviation) : this()
        {
            Name = name;
            ShortName = abbreviation;
        }

        public void AddWorkflow(string name, WorkflowType wftype)
        {
            var existingWorkflow = _workflows
                .Where(s => String.Equals(s.Name.ToLowerInvariant(), name.ToLowerInvariant()))
                .SingleOrDefault();
            if(existingWorkflow == null)
            {
                _workflows.Add(new Workflow(name, wftype));
            }
        }
    }
}
