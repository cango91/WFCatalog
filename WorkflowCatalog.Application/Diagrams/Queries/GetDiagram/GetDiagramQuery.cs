using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Diagrams.Queries.GetDiagram
{
    public class GetDiagramQuery : IRequest<DiagramDto>
    {
        public int Id { get; set; }
    }
    public class GetDiagramQueryHandler : IRequestHandler<GetDiagramQuery, DiagramDto>
    {
        private readonly IApplicationDbContext _context;
        public GetDiagramQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DiagramDto> Handle(GetDiagramQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagrams.FindAsync(query.Id);
            if(entity == null)
            {
                throw new NotFoundException(nameof(WorkflowDiagram),query.Id);
            }

            return new DiagramDto
            {
                Name =  entity.Name,
                MimeType  = entity.MimeType,
                File = entity.File
            };

        }
    }

}
