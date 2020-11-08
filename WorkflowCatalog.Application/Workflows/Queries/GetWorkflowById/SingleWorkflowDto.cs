using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class SingleWorkflowDto : Auditable, IMapFrom<Workflow>
    {
        public Guid Id { get; set; }
        public Guid SetupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkflowType { get; set; }
        public List<Guid> UseCaseIds { get; set; }
        public List<Guid> DiagramIds { get; set; }
        public Guid PrimaryDiagramId { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<Workflow, SingleWorkflowDto>()
                .ForMember(x => x.SetupId, opt => opt.MapFrom(x => x.Setup.Id))
                .ForMember(x => x.WorkflowType, opt => opt.MapFrom(x => (int)x.Type))
                .ForMember(x => x.PrimaryDiagramId, opt => opt.MapFrom(x => x.Primary.Id))
                .ForMember(x => x.UseCaseIds, opt => opt.MapFrom(x => x.UseCases.Select(x => x.Id)))
                .ForMember(x => x.DiagramIds, opt => opt.MapFrom(x => x.Diagrams.Select(x => x.Id)));
        }
    }
}
