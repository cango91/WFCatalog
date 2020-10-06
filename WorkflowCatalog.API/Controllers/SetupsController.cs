using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Setups.Commands.CreateSetup;
using WorkflowCatalog.Application.Setups.Commands.DeleteSetup;
using WorkflowCatalog.Application.Setups.Queries.GetSetupById;
using WorkflowCatalog.Application.Setups.Queries.GetSetups;

namespace WorkflowCatalog.API.Controllers
{
   [Authorize] 
    public class SetupsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SetupsVm>> Get()
        {
            return await Mediator.Send(new GetSetupsQuery());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SetupVm>> GetById(int id)
        {
            return await Mediator.Send(new GetSetupByIdQuery { Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSetupCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(DeleteSetupCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
