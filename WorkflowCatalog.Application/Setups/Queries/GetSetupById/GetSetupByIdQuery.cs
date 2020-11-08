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

namespace WorkflowCatalog.Application.Setups.Queries.GetSetupById
{
    public class GetSetupByIdQuery : IRequest<SingleSetupDto>
    {
        public Guid Id { get; set; }
    }

    public class GetSetupByIdQueryHandler : IRequestHandler<GetSetupByIdQuery,SingleSetupDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetSetupByIdQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SingleSetupDto> Handle (GetSetupByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Setups
                .AsNoTracking()
                .ProjectTo<SingleSetupDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == query.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Setup), query.Id);
            }

            return entity;
        }
    }
}
