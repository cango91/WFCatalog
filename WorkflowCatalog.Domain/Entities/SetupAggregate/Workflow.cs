using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Enums;
using WorkflowCatalog.Domain.Exceptions;

namespace WorkflowCatalog.Domain.Entities.SetupAggregate
{
    public class Workflow : AuditableEntity
    {
        public string Name{ get; private set;}
        public string Description { get; private set; }
        public WorkflowType Type { get; private set; }

        private List<WorkflowDiagram> _diagrams;
        private List<UseCase> _usecases;
        
        public IReadOnlyCollection<WorkflowDiagram> Diagrams => _diagrams;
        public IReadOnlyCollection<UseCase> UseCases => _usecases;

        protected Workflow()
        {
            _diagrams = new List<WorkflowDiagram>();
            _usecases = new List<UseCase>();
        }
        public Workflow(string name, WorkflowType wftype) : this()
        {
            Name = name;
            Type = wftype;
        }
        public Workflow(string name,string description, WorkflowType wftype) : this(name,wftype)
        {
            Description = description;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetType(WorkflowType newtype)
        {
            Type = newtype;
        }
        public WorkflowDiagram AddDiagram(string filename, string filetype, byte[] diagram)
        {
            var existingDiagramForWorkflow = _diagrams.
                Where(s => s.Filename == filename && s.FileType == filetype && s.GetOwner == this)
                .SingleOrDefault();
            if(existingDiagramForWorkflow != null)
            {
                throw new WorkflowCatalogDomainException($"{filename}.{filetype} already exists for this workflow");
            }
            var workflowDiagram = new WorkflowDiagram(this, filename, diagram, filetype, _diagrams.Count == 0 ? true : false);
            _diagrams.Add(workflowDiagram);
            return workflowDiagram;
        }

        public void AddUseCase()
        {

        }
    }
}
