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
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflows
{
    public class GetWorkflowsQuery : SieveModel, IRequest<WorkflowsVm>
    {
        public GetWorkflowsQuery()
        {
        }
    }
    public class GetWorkflowsQueryHandler : IRequestHandler<GetWorkflowsQuery,WorkflowsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SieveProcessor _processor;

        public GetWorkflowsQueryHandler(IApplicationDbContext context, IMapper mapper, SieveProcessor processor)
        {
            _processor = processor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<WorkflowsVm> Handle(GetWorkflowsQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Workflows
                .ProjectTo<WorkflowsDto>(_mapper.ConfigurationProvider);

            return new WorkflowsVm
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
