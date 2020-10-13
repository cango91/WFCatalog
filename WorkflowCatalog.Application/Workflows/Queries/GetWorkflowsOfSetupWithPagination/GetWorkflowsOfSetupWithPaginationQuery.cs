using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowsOfSetupWithPagination
{
    public class GetWorkflowsOfSetupWithPaginationQuery : SieveModel, IRequest<IList<SingleWorkflowDto>>
    {
        public int SetupId { get; set; }
        //public int PageNumber { get; set; }
        //public int PageSize { get; set; } = 6;
        //public string SortBy { get; set; }
        //public string SortOrder { get; set; }
        //public List<int> FilterTypes { get; set; }
    }

    public class GetWorkflowsOfSetupWithPaginationQueryHandler : IRequestHandler<GetWorkflowsOfSetupWithPaginationQuery,IList<SingleWorkflowDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieve;

        public GetWorkflowsOfSetupWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _context = context;
            _sieve = sieveProcessor;
        }

        public async Task<IList<SingleWorkflowDto>> Handle(GetWorkflowsOfSetupWithPaginationQuery request,CancellationToken cancellationToken)
        {
            var results = _context.Workflows
                .Where(x => x.Setup.Id == request.SetupId);
            var sieveModel = new SieveModel
            {
                Page = request.Page,
                PageSize = request.Page,
                Sorts = request.Sorts,
                Filters = request.Filters
            };
            results = _sieve.Apply(sieveModel, results);
            return results.ProjectTo<SingleWorkflowDto>(_mapper.ConfigurationProvider).ToList();
            

        }
    }
}
