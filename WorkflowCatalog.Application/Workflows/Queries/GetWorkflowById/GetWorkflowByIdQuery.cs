using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class GetWorkflowByIdQuery : IRequest<SingleWorkflowDto>
    {
        public Guid Id { get; set; }
    }

    public class GetWorkflowByIdQueryHandler : IRequestHandler<GetWorkflowByIdQuery, SingleWorkflowDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetWorkflowByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SingleWorkflowDto> Handle(GetWorkflowByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Workflows
                .AsNoTracking()
                .ProjectTo<SingleWorkflowDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if(entity==null)
            {
                throw new NotFoundException(nameof(Workflow), query.Id);
            }

            return entity;

        }
    }
}
