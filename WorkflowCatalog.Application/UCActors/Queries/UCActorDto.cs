using System;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UCActors.Queries
{
    public class UCActorDto : IMapFrom<UseCaseActor>
    {

        [Sieve(CanFilter = true, CanSort = false)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UseCaseActor, UCActorDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(d => d.Id));
               
        }
    }
}
