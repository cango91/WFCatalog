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
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflows;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Workflows.Queries.DumpWorkflows
{
    public class DumpWorkflowsQuery : SieveModel, IRequest<WorkflowsDumpVm>
    {
    }

    public class DumpWorkflowsQueryHandler : IRequestHandler<DumpWorkflowsQuery,WorkflowsDumpVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;

        public DumpWorkflowsQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<WorkflowsDumpVm> Handle(DumpWorkflowsQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Workflows
                .ProjectTo<WorkflowsDumpDto>(_mapper.ConfigurationProvider);
            return new WorkflowsDumpVm
            {
                WorkflowTypes = Enum.GetValues(typeof(WorkflowType))
                .Cast<WorkflowType>()
                .Select(p => new WorkflowTypeDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

                Workflows = await results.Paginate(_processor, query)
            };
        }
    }
}
