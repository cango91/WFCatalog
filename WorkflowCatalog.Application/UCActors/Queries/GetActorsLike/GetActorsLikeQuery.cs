using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.UCActors.Queries.GetActorsLike
{
    public class GetActorsLikeQuery : IRequest<List<UCActorDto>>
    {
        public string Name { get; set; }
    }

    public class GetActorsLikeQueryHandler : IRequestHandler<GetActorsLikeQuery,List<UCActorDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetActorsLikeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UCActorDto>> Handle (GetActorsLikeQuery request, CancellationToken cancellationToken)
        {
            return await _context.Actors
                .ProjectTo<UCActorDto>(_mapper.ConfigurationProvider)
                .Where(x => x.Name.ToLower().Contains(request.Name.ToLower()))
                .ToListAsync();
        }
    }
}
