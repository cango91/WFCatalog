using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities.SetupAggregate
{
    public class UseCase : AuditableEntity
    {
        public int UseCaseId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Preconditions { get; private set; }
        public string Postconditions { get; private set; }
        public string NormalCourse { get; private set; }
        public string AltCourse { get; private set; }
        


        private readonly List<UseCaseActor> _usecaseActors;
        public IReadOnlyCollection<UseCaseActor> UseCaseActors => _usecaseActors;
        
        public void AddUseCaseActor(string name)
        {
            var existingUseCaseActor = _usecaseActors
                .Where(s => String.Equals(s.Name.ToLowerInvariant(), name))
                .SingleOrDefault();
            if(existingUseCaseActor == null)
            {
                _usecaseActors.Add(new UseCaseActor(name));
            }
        }

        private Workflow _workflow;
        public Workflow GetOwner => _workflow;

    }
}
