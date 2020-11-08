using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.UCActors.Commands.CreateActor;
using WorkflowCatalog.Application.UCActors.Commands.DeleteActor;
using WorkflowCatalog.Application.UCActors.Commands.UpdateActor;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Application.UCActors.Queries.GetActorById;
using WorkflowCatalog.Application.UCActors.Queries.GetActors;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class ActorsController : ApiController
    {
        [HttpGet]
        public async Task<PaginatedList<UCActorDto>> GetActors([FromQuery] GetActorsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{actorId}")]
        public async Task<UCActorDto> GeActortById(Guid actorId)
        {
            return await Mediator.Send(new GetActorByIdQuery { Id = actorId });
        }

        [HttpPost("forSetup/{setupId}")]
        public async Task<ActionResult<Guid>> CreateActor(Guid setupId, CreateActorCommand command)
        {
            if(command.SetupId != setupId)
            {
                return BadRequest();
            }
            return await Mediator.Send(command);
        }

        [HttpPut("{actorId}")]
        public async Task<ActionResult> UpdateActor(Guid actorId, UpdateActorCommand command)
        {
            if(actorId != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{actorId}")]
        public async Task<ActionResult<Unit>> DeleteActor(Guid actorId, DeleteActorCommand command)
        {
            if(actorId!=command.Id)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }
        
    }
}
