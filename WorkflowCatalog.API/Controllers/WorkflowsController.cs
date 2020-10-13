using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Workflows.Commands.CreateWorkflow;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflowsOfSetupWithPagination;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class WorkflowsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateWorkflowCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleWorkflowDto>> GetSingleWorkflowAsync(int id)
        {
            return await Mediator.Send(new GetWorkflowByIdQuery { Id = id });
        }

        [HttpGet("of/{setupId}")]
        public async Task<ActionResult<PaginatedList<SingleWorkflowDto>>> GetSetupsOf(int setupId,
            [FromQuery] SieveRequest request)
        {
            /*
            var filterTypes = request.filterTypes ?? new int[2] { (int) WorkflowType.MainFlow, (int) WorkflowType.SubFlow };
            return await Mediator.Send(new GetWorkflowsOfSetupWithPaginationQuery
            {
                SetupId = setupId,
                PageNumber = request.page,
                PageSize = request.pageSize,
                FilterTypes = filterTypes.ToList(),
                SortBy = request.sortBy,
                SortOrder = request.order
            });
            */
            return await Mediator.Send(new GetWorkflowsOfSetupWithPaginationQuery
            {
                SetupId = setupId,
                Page = request.Page,
                PageSize = request.PageSize,
                Filters = request.Filters,
                Sorts = request.Sorts
            });

        }

        
    }
}
