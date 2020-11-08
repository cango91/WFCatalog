using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Extensions;

namespace WorkflowCatalog.Application.Diagrams.Queries.GetDiagramsMetaData
{
    public class GetDiagramsMetaQuery : SieveModel, IRequest<PaginatedList<DiagramsMetaDto>>
    {
    }
    public class GetDiagramsMetaQueryHandler : IRequestHandler<GetDiagramsMetaQuery,PaginatedList<DiagramsMetaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;
        public GetDiagramsMetaQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<PaginatedList<DiagramsMetaDto>> Handle(GetDiagramsMetaQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Diagrams.AsNoTracking().ProjectTo<DiagramsMetaDto>(_mapper.ConfigurationProvider);
            return await results.Paginate(_processor, query);
        }
    }
}
