using System;
using MediatR;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class GetWorkflowByIdQuery : IRequest<SingleWorkflowDto>
    {
        public int Id { get; set; }
    }
}
