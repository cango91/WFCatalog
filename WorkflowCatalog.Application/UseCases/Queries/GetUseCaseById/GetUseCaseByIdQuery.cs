using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Exceptions;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.UseCases.Queries.GetUseCaseById
{
    public class GetUseCaseByIdQuery : IRequest<UseCaseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetUseCaseByIdQueryHandler : IRequestHandler<GetUseCaseByIdQuery,UseCaseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUseCaseByIdQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UseCaseDto> Handle(GetUseCaseByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.UseCases
                .AsNoTracking()
                .ProjectTo<UseCaseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(UseCase), query.Id);
            }

            return entity;
        }
    }
}
