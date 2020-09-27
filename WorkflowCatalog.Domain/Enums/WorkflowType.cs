using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowCatalog.Domain.Common;
using WorkflowCatalog.Domain.Exceptions;

namespace WorkflowCatalog.Domain.Enums
{
    public class WorkflowType : Enumeration
    {
        public static WorkflowType MainFlow = new WorkflowType(1, nameof(MainFlow).ToLowerInvariant());
        public static WorkflowType SubFlow = new WorkflowType(2, nameof(SubFlow).ToLowerInvariant());
        public WorkflowType(int id, string name) : base(id,name)
        {
        }

        public static IEnumerable<WorkflowType> List() =>
            new[] { MainFlow, SubFlow };
        public static WorkflowType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if(state == null)
            {
                throw new WorkflowCatalogDomainException($"Possible values for WorkflowType: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }
        public static WorkflowType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if(state==null)
            {
                throw new WorkflowCatalogDomainException($"Possible values for WorkflowType: {String.Join(",", List().Select(s => s.Name))}");
            }
            return state;
        }


    }
}
