using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Setups.Queries.GetSetups
{
    public class GetSetupsQuery : IRequest<SetupsVm>
    {
    }
    public class GetSetupsQueryHandler : IRequestHandler<GetSetupsQuery,SetupsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSetupsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SetupsVm> Handle(GetSetupsQuery request, CancellationToken cancellationToken)
        {
            return new SetupsVm
            {
                SetupStatus = Enum.GetValues(typeof(SetupStatus))
                .Cast<SetupStatus>()
                .Select(p => new SetupStatusDto { Value = (int)p,Name=p.ToString() } )
                .ToList(),

                Setups = await _context.Setups
                .ProjectTo<SetupsDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken)
            };
        }

    }
}
