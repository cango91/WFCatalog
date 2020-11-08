using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Diagrams.Queries.GetDiagramById
{
    public class GetDiagramByIdQuery : IRequest<DiagramDto>
    {
        public Guid Id { get; set; }
    }
    public class GetDiagramByIdQueryHandler : IRequestHandler<GetDiagramByIdQuery,DiagramDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetDiagramByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DiagramDto> Handle(GetDiagramByIdQuery query,CancellationToken cancellationToken)
        {
            var entity = await _context.Diagrams.AsNoTracking()
                .ProjectTo<DiagramDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(WorkflowDiagram), query.Id);
            }
            return entity;
        }
    }
}
