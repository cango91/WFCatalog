using System;
using AutoMapper;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class DiagramDto :IMapFrom<WorkflowDiagram>
    {
        public int Id { get; set; }
        //public int WorkflowId { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public bool IsPrimaryDiagram { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WorkflowDiagram, DiagramDto>()
                //.ForMember(d => d.WorkflowId, opt => opt.MapFrom(x => x.Workflow.Id))
                .ForMember(d => d.IsPrimaryDiagram, opt => opt.MapFrom(x => x.Id == x.Workflow.Primary.Id));

        }
    }
}
