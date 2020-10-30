using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.UseCases.Commands.CreateUseCase;
using WorkflowCatalog.Application.UseCases.Commands.DeleteUseCase;
using WorkflowCatalog.Application.UseCases.Commands.UpdateUseCaseDetails;
using WorkflowCatalog.Application.UseCases.Queries.GetUseCases;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class UseCasesController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateUseCaseCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update (int id, UpdateUseCaseDetailsCommand command)
        {
            if(id!=command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete (int id, DeleteUseCaseCommand command)
        {
            if(id!=command.Id)
            {
                return BadRequest();
            }
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<UseCasesDto>>> Get([FromQuery] GetUseCasesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaginatedList<UseCasesDto>>> GetSingle(int id)
        {
            return await Mediator.Send(new GetUseCasesQuery {
                Filters = $"Id=={id}"
            });
        }

    }
}
