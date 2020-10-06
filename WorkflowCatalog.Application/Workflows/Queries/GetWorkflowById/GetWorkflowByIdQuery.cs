using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class GetWorkflowByIdQuery : IRequest<SingleWorkflowDto>
    {
        public int Id { get; set; }
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

        public async Task<SingleWorkflowDto> Handle(GetWorkflowByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Workflows
                .Where(x => x.Id == request.Id)
                .ProjectTo<SingleWorkflowDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
            if(entity == null)
            {
                throw new NotFoundException(nameof(WorkflowCatalog.Domain.Entities.Workflow), request.Id);
            }
            return entity;
        }


    }
}
