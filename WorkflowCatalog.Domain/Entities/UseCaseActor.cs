using System;
using System.Collections.Generic;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities
{
    public class UseCaseActor : AuditableEntity
    {
        public Guid UseCaseId { get; set; }
        public UseCase UseCase { get; set; }
        public Guid ActorId { get; set; }
        public Actor Actor { get; set; }

        public UseCaseActor()
        {

        }
    }
}
