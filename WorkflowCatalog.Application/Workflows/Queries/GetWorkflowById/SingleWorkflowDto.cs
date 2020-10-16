using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sieve.Attributes;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class SingleWorkflowDto : IMapFrom<Workflow>
    {
        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int Type { get; set; }


        public List<UseCasesDto> UseCases { get; set; }
        public List<DiagramDto> Diagrams { get; set; }


        public int SetupId { get; set; }

        public int PrimaryDiagramId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Workflow, SingleWorkflowDto>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => (int)s.Type))
                .ForMember(d => d.PrimaryDiagramId, opt => opt.MapFrom(x => x.Primary.Id));
        }
    }


}
