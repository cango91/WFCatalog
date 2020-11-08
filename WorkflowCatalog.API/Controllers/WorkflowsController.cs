using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Workflows.Commands.CreateWorkflow;
using WorkflowCatalog.Application.Workflows.Commands.DeleteWorkflow;
using WorkflowCatalog.Application.Workflows.Commands.UpdateWorkflowDetails;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflows;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class WorkflowsController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleWorkflowDto>> GetWorkflowById(Guid id)
        {
            return await Mediator.Send(new GetWorkflowByIdQuery
            {
                Id = id
            });
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<WorkflowsDto>>> GetWorkflows([FromQuery] GetWorkflowsQuery query)
        {
            return await Mediator.Send(query);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateWorkflow(CreateWorkflowCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWorkflow (Guid id, UpdateWorkflowDetailsCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteWorkflow(Guid id,DeleteWorkflowCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return await Mediator.Send(command);
        }

     /*   [HttpPost("{id}/copy")]
        public async Task<ActionResult<int>> CopyWorkflow(int id,CopyWorkflowCommand command)
        {
            if(id!=command.WorkflowId)
            {
                return BadRequest();
            }
            return await Mediator.Send(command);
        }*/

        
    }
}
