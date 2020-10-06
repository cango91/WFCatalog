﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Workflows.Commands.CreateWorkflow;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById;

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
        public async Task<ActionResult<PaginatedList>> smt()
        {

        }
    }
}
