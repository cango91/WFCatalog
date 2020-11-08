using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Diagrams.Queries.GetDiagramById
{
    public class DiagramDto : Auditable, IMapFrom<WorkflowDiagram>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WorkflowId { get; set; }
        public string ContentType { get; set; }
        public byte[] File { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<WorkflowDiagram, DiagramDto>()
                .ForMember(x => x.WorkflowId, opt => opt.MapFrom(x => x.Workflow.Id));
        }
    }
}
