using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Extensions;
using WorkflowCatalog.Application.Setups.Queries.GetSetups;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Setups.Queries.DumpSetups
{
    public class DumpSetupsQuery : SieveModel, IRequest<SetupsDumpVm>
    {
    }

    public class DumpSetupsQueryHandler : IRequestHandler<DumpSetupsQuery,SetupsDumpVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;
        public DumpSetupsQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<SetupsDumpVm> Handle(DumpSetupsQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Setups
                .ProjectTo<SetupsDumpDto>(_mapper.ConfigurationProvider);
            return new SetupsDumpVm
            {
                SetupStatuses = Enum.GetValues(typeof(SetupStatus))
                .Cast<SetupStatus>()
                .Select(p => new SetupStatusDto { Value = (int)p, Name = p.ToString() })
                .ToList(),
                Setups = await results.Paginate(_processor, query)
            };
        }
    }
}
