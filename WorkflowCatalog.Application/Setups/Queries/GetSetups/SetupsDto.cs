using System;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Setups.Queries.GetSetups
{
    public class SetupsDto : Auditable, IMapFrom<Setup>
    {

        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string ShortName { get; set; }

        [Sieve(CanFilter = true, CanSort = false)]
        public int Status { get; set; }

        [Sieve(CanFilter = false, CanSort = true)]
        public int WorkflowCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Setup, SetupsDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => (int)s.Status))
                .ForMember(d => d.WorkflowCount, opt => opt.MapFrom(s => s.Workflows.Count));
        }
    }
}
