using System;
using System.Collections.Generic;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Queries.GetUseCases
{
    public class UseCasesDto : Auditable, IMapFrom<UseCase>
    {
        [Sieve(CanFilter =true, CanSort =true)]
        public int Id { get; set; }

        [Sieve(CanSort =true,CanFilter =true)]
        public string Name { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public string Description { get; set; }

        //[Sieve(CanSort = true, CanFilter = true)]
        public List<UCActorDto> Actors { get; set; }

        [Sieve(CanSort = false, CanFilter = false)]
        public string Preconditions { get; set; }

        [Sieve(CanSort = false, CanFilter = false)]
        public string Postconditions { get; set; }

        [Sieve(CanSort = false, CanFilter = false)]
        public string NormalCourse { get; set; }

        [Sieve(CanSort = false, CanFilter = false)]
        public string AltCourse { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public int WorkflowId { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public int SetupId { get; set; }

        [Sieve(CanSort = false, CanFilter = false)]
        public string WorkflowName { get; set; }

        [Sieve(CanSort = false, CanFilter = false)]
        public string SetupName { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<UseCase, UseCasesDto>()
                .ForMember(x => x.WorkflowId, opt => opt.MapFrom(x => x.Workflow.Id))
                .ForMember(x => x.SetupId, opt => opt.MapFrom(x => x.Workflow.Setup.Id))
                .ForMember(x => x.SetupName, opt => opt.MapFrom(x => x.Workflow.Setup.ShortName))
                .ForMember(x => x.WorkflowName, opt=> opt.MapFrom(x => x.Workflow.Name));
        }

    }
}
