using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Extensions;

namespace WorkflowCatalog.Application.UCActors.Queries.GetActors
{
    public class GetActorsQuery : IRequest<IList<UCActorDto>>
    {
        public string Filters { get; set; }
        public string Sorts { get; set; }
    }
    
    public class GetActorsQueryHandler : IRequestHandler<GetActorsQuery,IList<UCActorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly SieveProcessor _processor;
        private readonly IMapper _mapper;

        public GetActorsQueryHandler(IApplicationDbContext context,SieveProcessor processor, IMapper mapper)
        {
            _context = context;
            _processor = processor;
            _mapper = mapper;
        }

        public async Task<IList<UCActorDto>> Handle (GetActorsQuery query, CancellationToken cancellationToken)
        {
            var actors = _context.Actors.ProjectTo<UCActorDto>(_mapper.ConfigurationProvider);
            return await actors.FilterAndSort(_processor,new SieveModel
            {
                Filters = query.Filters,
                Sorts = query.Sorts
            });

        }
    }
    
}
