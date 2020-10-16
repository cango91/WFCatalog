using System;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflows
{
    public class WorkflowsDto : Auditable, IMapFrom<Workflow>
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int Type { get; set; }


        [Sieve(CanFilter = true, CanSort = true)]
        public int SetupId { get; set; }

        [Sieve(CanFilter = false, CanSort = false)]
        public int PrimaryDiagramId { get; set; }

        [Sieve(CanSort = true, CanFilter = false)]
        public int DiagramCount { get; set; }

        [Sieve(CanFilter = false, CanSort = true)]
        public int UseCaseCount { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Workflow, WorkflowsDto>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => (int)s.Type))
                .ForMember(d => d.PrimaryDiagramId, opt => opt.MapFrom(x => x.Primary.Id))
                .ForMember(d => d.DiagramCount,opt => opt.MapFrom(x=>x.Diagrams.Count))
                .ForMember(d => d.UseCaseCount, opt => opt.MapFrom(x =>x.UseCases.Count));
        }
    }
}
