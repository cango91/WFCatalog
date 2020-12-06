using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Extensions;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflows
{
    public class GetWorkflowsQuery : SieveModel, IRequest<PaginatedList<WorkflowsDto>>
    {
    }

    public class GetWorkflowsQueryHandler : IRequestHandler<GetWorkflowsQuery, PaginatedList<WorkflowsDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _processor;

        public GetWorkflowsQueryHandler(IApplicationDbContext context, IMapper mapper, ApplicationSieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }

        public async Task<PaginatedList<WorkflowsDto>> Handle (GetWorkflowsQuery query, CancellationToken cancellationToken)
        {
            var result = _context.Workflows
                .AsNoTracking()
                .ProjectTo<WorkflowsDto>(_mapper.ConfigurationProvider);
            return await result.Paginate(_processor, query);
        }
    }
}
