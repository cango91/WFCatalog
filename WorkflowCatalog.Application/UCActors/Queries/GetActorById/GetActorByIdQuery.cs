using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UCActors.Queries.GetActorById
{
    public class GetActorByIdQuery : IRequest<ActorDto>
    {
        public Guid Id { get; set; }
    }
    public class GetActorByIdQueryHandler: IRequestHandler<GetActorByIdQuery,ActorDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetActorByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActorDto> Handle(GetActorByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Actors
                .AsNoTracking()
                .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Actor), query.Id);
            }

            return entity;
        }
    }
}
