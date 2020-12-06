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
using WorkflowCatalog.Application.Common;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Extensions;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UCActors.Queries.GetActors
{
    public class GetActorsQuery : SieveModel, IRequest<PaginatedList<ActorDto>>
    {
    }

    public class GetActorsQueryHandler : IRequestHandler<GetActorsQuery,PaginatedList<ActorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _processor;

        public GetActorsQueryHandler(IApplicationDbContext context, IMapper mapper, ApplicationSieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<PaginatedList<ActorDto>> Handle(GetActorsQuery query, CancellationToken cancellationToken)
        {
            var entity = _context.Actors
                .Include(x => x.Setup)
                .AsNoTracking();
                //.ProjectTo<ActorDto>(_mapper.ConfigurationProvider);
            return await entity.Paginate<Actor,ActorDto>(_processor, query,_mapper);

        }
    }
}
