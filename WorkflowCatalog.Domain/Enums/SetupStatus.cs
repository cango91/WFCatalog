using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Exceptions;

namespace WorkflowCatalog.Domain.Enums
{
    public class SetupStatus : Enumeration
    {
        public static SetupStatus Passive = new SetupStatus(0, nameof(Passive).ToLowerInvariant());
        public static SetupStatus Active = new SetupStatus(1, nameof(Active).ToLowerInvariant());

        public SetupStatus(int id, string name) : base(id,name)
        {
        }

        public static IEnumerable<SetupStatus> List() =>
            new[] { Passive, Active };

        public static SetupStatus operator !(SetupStatus status)
        {
            return status == SetupStatus.Active ? SetupStatus.Passive : SetupStatus.Active;
        }

        public static SetupStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
            {
                throw new WorkflowCatalogDomainException($"Possible values for SetupStatus: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;

        }

        public static SetupStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
            {
                throw new WorkflowCatalogDomainException($"Possible values for SetupStatus: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }

        public static implicit operator int(SetupStatus s) => s.Id;
        //public static implicit operator string(SetupStatus s) =>  s.Name;
      
    }
}
