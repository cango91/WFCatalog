using AutoMapper;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.ValueObjects;

namespace WorkflowCatalog.Application.Diagrams.Queries.GetDiagramsMetaData
{
    public class DiagramsMetaDto : Auditable, IMapFrom<WorkflowDiagram>
    {
        [Sieve(CanSort =true, CanFilter =true)]
        public Guid WorkflowId { get; set; }
        [Sieve(CanSort = true, CanFilter = true)]
        public Guid Id { get; set; }
        [Sieve(CanSort = true, CanFilter = true)]
        public string Name { get; set; }
        public string ContentType { get; set; }
        
        

        public void Configure(Profile profile)
        {
            profile.CreateMap<WorkflowDiagram, DiagramsMetaDto>()
                //.ForMember(x => x.Filename, opt => opt.MapFrom(x => x.Name))
                //.ForMember(x => x.FileId, opt=>opt.MapFrom(x =>x.Id))
                .ForMember(x => x.WorkflowId, opt => opt.MapFrom(x => x.Workflow.Id));
                
        }
    }
}
