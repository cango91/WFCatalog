using System;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Diagrams.Queries.GetDiagram
{
    public class DiagramDto : IMapFrom<WorkflowDiagram>
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] File { get; set; }
    }
}
