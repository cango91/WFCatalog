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

namespace WorkflowCatalog.Application.UseCases.Queries.GetUseCases
{
    public class GetUseCasesQuery : SieveModel, IRequest<PaginatedList<UseCasesDto>>
    {
        public GetUseCasesQuery()
        {
        }
    }

    public class GetUseCasesQueryHandler : IRequestHandler<GetUseCasesQuery,PaginatedList<UseCasesDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;
        public GetUseCasesQueryHandler(IApplicationDbContext context, SieveProcessor processor, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _sieveProcessor = processor;
        }

        public async Task<PaginatedList<UseCasesDto>> Handle (GetUseCasesQuery query,CancellationToken cancellationToken)
        {
            var results = _context.UseCases
                .ProjectTo<UseCasesDto>(_mapper.ConfigurationProvider);

            return await results.Paginate(_sieveProcessor, query);
        }
    }
}
