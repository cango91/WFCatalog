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

namespace WorkflowCatalog.Application.UseCases.Queries.DumpUseCases
{
    public class DumpUseCasesQuery : SieveModel, IRequest<PaginatedList<UseCasesDumpDto>>
    {
    }

    public class DumpUseCasesQueryHandler : IRequestHandler<DumpUseCasesQuery, PaginatedList<UseCasesDumpDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;

        public DumpUseCasesQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<PaginatedList<UseCasesDumpDto>> Handle (DumpUseCasesQuery query, CancellationToken cancellationToken)
        {
            var results = _context.UseCases
                .ProjectTo<UseCasesDumpDto>(_mapper.ConfigurationProvider);
            return await results.Paginate(_processor, query);
        }


    }
}
