using AutoMapper;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Queries
{
    public class UseCaseDto : Auditable, IMapFrom<UseCase>
    {
        [Sieve(CanFilter = true, CanSort = false)]
        public Guid Id { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid WorkflowId { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        public List<UCActorDto> Actors { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Preconditions { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Postconditions { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string NormalCourse { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string AltCourse { get; set; }

        public UseCaseDto()
        {
            Actors = new List<UCActorDto>();
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UseCase, UseCaseDto>()
                .ForMember(x => x.WorkflowId, opt => opt.MapFrom(x => x.Workflow.Id))
                .ForMember(x => x.Actors, opt => opt.MapFrom(x => x.UseCaseActors.Select(uca => uca.Actor)));
        }


    }
}
