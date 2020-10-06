using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Workflows.Commands.CreateWorkflow;

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
    }
}
