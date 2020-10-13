using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class UseCasesDto : IMapFrom<UseCase>
    {

        [Sieve(CanFilter = false, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        
        public IList<UCActorDto> Actors { get; set; }

        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UseCase, UseCasesDto>();
                //.ForMember(d => d.Actors, opt => opt.MapFrom(s => s.Actors.Select(x => x.Name).ToList()))
                //.ForMember(d => d.Actors, opt => opt.MapFrom(s => s.Actors.Select(x =>x.Name).ToList()));
        }
        
    }
}
