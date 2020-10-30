using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Extensions;

namespace WorkflowCatalog.Application.Diagrams.Queries.DumpDiagramsInfo
{
    public class DumpDiagramsInfoQuery : SieveModel, IRequest<PaginatedList<DiagramsInfoDto>>
    {
    }

    public class DumpDiagramsInfoQueryHandler :  IRequestHandler<DumpDiagramsInfoQuery,PaginatedList<DiagramsInfoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;

        public DumpDiagramsInfoQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<PaginatedList<DiagramsInfoDto>> Handle(DumpDiagramsInfoQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Diagrams
                .ProjectTo<DiagramsInfoDto>(_mapper.ConfigurationProvider);
            return await results.Paginate(_processor, query);
        }
    }
}
