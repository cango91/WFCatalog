using System;
using AutoMapper;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Diagrams.Queries.DumpDiagramsInfo
{
    public class DiagramsInfoDto : Auditable, IMapFrom<WorkflowDiagram>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public int WorkflowId { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<WorkflowDiagram, DiagramsInfoDto>()
                .ForMember(x => x.WorkflowId, opt => opt.MapFrom(x => x.Workflow.Id));
                
        }
    }
}
