using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Queries.DumpUseCases
{
    public class UseCasesDumpDto : Auditable, IMapFrom<UseCase>
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int WorkflowId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        public List<int> ActorIds { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Preconditions { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Postconditions { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string NormalCourse { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string AltCourse { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<UseCase, UseCasesDumpDto>()
                .ForMember(x => x.WorkflowId, opt => opt.MapFrom(x => x.Workflow.Id))
                .ForMember(x => x.ActorIds, opt => opt.MapFrom(x => x.Actors.Select(x => x.Id)));
        }

    }
}
