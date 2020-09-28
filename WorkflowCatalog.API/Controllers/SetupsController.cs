using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Setups.Commands.CreateSetup;
using WorkflowCatalog.Application.Setups.Queries.GetSetups;

namespace WorkflowCatalog.API.Controllers
{
    
    public class SetupsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SetupsVm>> Get()
        {
            return await Mediator.Send(new GetSetupsQuery());
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSetupCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
