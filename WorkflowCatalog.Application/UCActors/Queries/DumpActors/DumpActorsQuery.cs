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

namespace WorkflowCatalog.Application.UCActors.Queries.DumpActors
{
    public class DumpActorsQuery : SieveModel, IRequest<PaginatedList<UCActorDto>>
    {
    }

    public class DumpActorsQueryHandler : IRequestHandler<DumpActorsQuery,PaginatedList<UCActorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;

        public DumpActorsQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<PaginatedList<UCActorDto>> Handle(DumpActorsQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Actors
                .ProjectTo<UCActorDto>(_mapper.ConfigurationProvider);

            return await results.Paginate(_processor, query);
        }
    }
}
