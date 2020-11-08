using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Extensions;

namespace WorkflowCatalog.Application.UseCases.Queries.GetUseCases
{
    public class GetUseCasesQuery : SieveModel, IRequest<PaginatedList<UseCaseDto>>
    {
    }

    public class GetUseCasesQueryHandler : IRequestHandler<GetUseCasesQuery,PaginatedList<UseCaseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;
        public GetUseCasesQueryHandler(IMapper mapper, IApplicationDbContext context, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }
        public async Task<PaginatedList<UseCaseDto>> Handle (GetUseCasesQuery query, CancellationToken cancellationToken)
        {
            var entity = _context.UseCases
                .AsNoTracking()
                .ProjectTo<UseCaseDto>(_mapper.ConfigurationProvider);
            return await entity.Paginate(_processor, query);
        }
    }
}
