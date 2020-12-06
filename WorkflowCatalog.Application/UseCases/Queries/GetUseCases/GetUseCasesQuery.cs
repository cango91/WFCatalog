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
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Queries.GetUseCases
{
    public class GetUseCasesQuery : SieveModel, IRequest<PaginatedList<UseCaseDto>>
    {
    }

    public class GetUseCasesQueryHandler : IRequestHandler<GetUseCasesQuery,PaginatedList<UseCaseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationSieveProcessor _processor;
        public GetUseCasesQueryHandler(IMapper mapper, IApplicationDbContext context, ApplicationSieveProcessor processor)
        {
            _context = context;
            _mapper = mapper;
            _processor = processor;
        }
        public async Task<PaginatedList<UseCaseDto>> Handle (GetUseCasesQuery query, CancellationToken cancellationToken)
        {
            var entity = _context.UseCases
                .AsNoTracking()
                .Include(x => x.UseCaseActors)
                .Include(x => x.Workflow);
/*                .Include(x => x.Actors)
                .Include(x => x.Workflow);*/

            return await entity.Paginate<UseCase,UseCaseDto>(_processor, query,_mapper);
        }
    }
}
