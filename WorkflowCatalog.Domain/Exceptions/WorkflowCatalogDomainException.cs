using System;

namespace WorkflowCatalog.Domain.Exceptions
{
    class WorkflowCatalogDomainException : Exception
    {
        public WorkflowCatalogDomainException() 
        {}
        public WorkflowCatalogDomainException(string msg) : base(msg) 
        {}
        public WorkflowCatalogDomainException(string msg, Exception ex) : base(msg, ex) 
        {}
    }
}
