using AutoMapper;
using Sieve.Attributes;
using System;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflows
{
    public class WorkflowsDto : Auditable, IMapFrom<Workflow>
    {
        [Sieve(CanFilter = true, CanSort = false)]
        public Guid Id { get; set; }
        [Sieve(CanSort = true,CanFilter = true)]
        public string Name { get; set; }
        [Sieve(CanSort = true, CanFilter = true)]
        public string Description { get; set; }
        [Sieve(CanSort = false, CanFilter = true)]
        public int WorkflowType { get; set; }
        [Sieve(CanSort = true, CanFilter = true)]
        public Guid SetupId { get; set; }
        [Sieve(CanSort = true, CanFilter = true)]
        public Guid? PrimaryDiagramId { get; set; }
        [Sieve(CanSort = true, CanFilter = true)]
        public int DiagramCount { get; set; }
        [Sieve(CanSort = true, CanFilter = true)]
        public int UseCaseCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Workflow, WorkflowsDto>()
                .ForMember(x => x.DiagramCount, opt => opt.MapFrom(x => x.Diagrams.Count))
                .ForMember(x => x.UseCaseCount, opt => opt.MapFrom(x => x.UseCases.Count))
                .ForMember(x => x.SetupId, opt => opt.MapFrom(x => x.Setup.Id))
                .ForMember(x => x.PrimaryDiagramId, opt => opt.MapFrom(x => x.Primary.Id))
                .ForMember(x => x.WorkflowType, opt => opt.MapFrom(x => (int) x.Type));
        }
    }
}
