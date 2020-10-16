using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Extensions;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Setups.Queries.GetSetups
{

    public class GetSetupsQuery : IRequest<SetupsVm>
    {
        public string Filters { get; set; }
        public string Sorts { get; set; }
    }
    public class GetSetupsQueryHandler : IRequestHandler<GetSetupsQuery,SetupsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;

        public GetSetupsQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<SetupsVm> Handle(GetSetupsQuery request, CancellationToken cancellationToken)
        {
            var setups =  _context.Setups
                .ProjectTo<SetupsDto>(_mapper.ConfigurationProvider);
            
            return new SetupsVm
            {
                SetupStatus = Enum.GetValues(typeof(SetupStatus))
                .Cast<SetupStatus>()
                .Select(p => new SetupStatusDto { Value = (int)p,Name=p.ToString() } )
                .ToList(),

                Setups = await setups.FilterAndSort(_processor, new SieveModel
                {
                    Filters = request.Filters,
                    Sorts = request.Sorts,
                })

        };
        }

    }
}
