using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Extensions;

namespace WorkflowCatalog.Application.UCActors.Queries.GetActors
{
    public class GetActorsQuery : SieveModel, IRequest<PaginatedList<UCActorDto>>
    {
    }

    public class GetActorsQueryHandler : IRequestHandler<GetActorsQuery,PaginatedList<UCActorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;

        public GetActorsQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<PaginatedList<UCActorDto>> Handle(GetActorsQuery query, CancellationToken cancellationToken)
        {
            var entity = _context.Actors
                .AsNoTracking()
                .ProjectTo<UCActorDto>(_mapper.ConfigurationProvider);
            return await entity.Paginate(_processor, query);

        }
    }
}
