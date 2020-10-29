using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.UCActors.Commands.AddActor;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Application.UCActors.Queries.GetActors;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class ActorsController : ApiController
    {
        [HttpGet]
        public async Task<IList<UCActorDto>> GetAll([FromQuery] GetActorsQuery query)
        {
            return await Mediator.Send(new GetActorsQuery
            {
                Filters = query.Filters,
                Sorts = query.Sorts
            });
        }

        [HttpPost]
        public async Task<ActionResult<UCActorDto>> AddActor(AddActorCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
