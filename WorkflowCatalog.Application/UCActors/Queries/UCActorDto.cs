using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UCActors.Queries
{
    public class UCActorDto : Auditable, IMapFrom<UseCaseActor>
    {

        [Sieve(CanFilter = true, CanSort = false)]
        public Guid Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid Setup { get; set; }
        [Sieve(CanFilter = false, CanSort = false)]
        public List<Guid> UseCases { get; set; }
        


            public void Mapping(Profile profile)
            {
                profile.CreateMap<UseCaseActor, UCActorDto>()
                    .ForMember(x => x.Setup, opt => opt.MapFrom(d => d.Setup.Id))
                    .ForMember(x => x.UseCases, opt => opt.MapFrom(d => d.UseCases.Select(x=>x.Id)));

            }


    }
}
