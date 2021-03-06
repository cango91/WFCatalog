﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Workflows.Commands.CreateWorkflow;
using WorkflowCatalog.Application.Workflows.Queries.GetWorkflows;
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
        public async Task<ActionResult<WorkflowsVm>> GetSingleWorkflowAsync(int id)
        {
            //return await Mediator.Send(new GetWorkflowByIdQuery { Id = id });
            return await Mediator.Send(new GetWorkflowsQuery
            {
                Filters = $"id=={id}"
            });
        }

        [HttpGet]
        public async Task<ActionResult<WorkflowsVm>> GetWorkflows ([FromQuery] GetWorkflowsQuery query)
        {
            return await Mediator.Send(query);
        }

        
    }
}
