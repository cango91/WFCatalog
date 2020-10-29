using System;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Application.Common.Models
{
    public abstract class Auditable
    {
        [Sieve(CanFilter = true, CanSort =true)]
        public string CreatedBy { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime Created { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string LastModifiedBy { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime LastModified { get; set; }
    
    }
}
