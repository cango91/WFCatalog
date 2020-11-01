using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Setups.Queries.DumpSetups
{
    public class SetupsDumpDto : Auditable, IMapFrom<Setup>
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string ShortName { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int SetupStatus { get; set; }

        public List<int> WorkflowIds { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<Setup, SetupsDumpDto>()
                .ForMember(x => x.SetupStatus, opt => opt.MapFrom(x => (int)x.Status))
                .ForMember(x => x.WorkflowIds, opt => opt.MapFrom(x => x.Workflows.Select(x => x.Id).ToList()));
        }
    }
}
