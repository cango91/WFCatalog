using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.Setups.Queries.GetSetupById
{
    public class GetSetupByIdQuery : IRequest<SetupVm>
    {
        public int Id { get; set; }
    }
    public class GetSetupByIdQueryHandler : IRequestHandler<GetSetupByIdQuery,SetupVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSetupByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SetupVm> Handle (GetSetupByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Setups
                .Where(c => c.Id == request.Id)
                .ProjectTo<SingleSetupDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(WorkflowCatalog.Domain.Entities.Setup), request.Id);
            }
            return new SetupVm
            {
                Setup = entity
            };
           
        }

    }
}
