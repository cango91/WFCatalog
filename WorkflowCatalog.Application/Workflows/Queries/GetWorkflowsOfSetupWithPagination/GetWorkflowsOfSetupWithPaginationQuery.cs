using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowsOfSetupWithPagination
{
    public class GetWorkflowsOfSetupWithPaginationQuery : IRequest<PaginatedList<SingleWorkflowDto>>
    {
        public int SetupId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public List<int> FilterTypes { get; set; }
    }

    public class GetWorkflowsOfSetupWithPaginationQueryHandler : IRequestHandler<GetWorkflowsOfSetupWithPaginationQuery,PaginatedList<SingleWorkflowDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWorkflowsOfSetupWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedList<SingleWorkflowDto>> Handle(GetWorkflowsOfSetupWithPaginationQuery request,CancellationToken cancellationToken)
        {
            return await _context.Workflows
                .Where(x => x.Setup.Id == request.SetupId)
                .OrderBy()
        }
    }
}
