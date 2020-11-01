using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.DumpWorkflows
{
    public class WorkflowsDumpDto : Auditable, IMapFrom<Workflow>
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int SetupId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int Type { get; set; }

        public List<int> UseCaseIds { get; set; }
        public List<int> DiagramIds { get; set; }

        [Sieve(CanFilter = true, CanSort = false)]
        public int? PrimaryDiagramId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Workflow, WorkflowsDumpDto>()
                .ForMember(x => x.SetupId, opt => opt.MapFrom(x => x.Setup.Id))
                .ForMember(x => x.Type, opt => opt.MapFrom(x => (int)x.Type))
                .ForMember(x => x.UseCaseIds, opt => opt.MapFrom(x => x.UseCases.Select(x => x.Id).ToList()))
                .ForMember(x => x.DiagramIds, opt => opt.MapFrom(x => x.Diagrams.Select(x => x.Id).ToList()))
                .ForMember(x => x.PrimaryDiagramId, opt => opt.MapFrom(x => x.Primary.Id));
        }
    }
}
