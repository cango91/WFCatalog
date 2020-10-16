using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Setups.Commands.CreateSetup;
using WorkflowCatalog.Application.Setups.Commands.DeleteSetup;
using WorkflowCatalog.Application.Setups.Commands.UpdateSetupDetails;
//using WorkflowCatalog.Application.Setups.Queries.GetSetupById;
using WorkflowCatalog.Application.Setups.Queries.GetSetups;

namespace WorkflowCatalog.API.Controllers
{
   [Authorize] 
    public class SetupsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SetupsVm>> Get([FromQuery] GetSetupsQuery query)
        {
            return await Mediator.Send(query);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SetupsVm>> GetById(int id)
        {
            //return await Mediator.Send(new GetSetupByIdQuery { Id = id});

            return await Mediator.Send(new GetSetupsQuery
            {
                Filters = $"id=={id}"
            });
        }



        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSetupCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateSetupDetailsCommand command)
        {
            if(id!=command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(DeleteSetupCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
