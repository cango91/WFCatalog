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
using WorkflowCatalog.Application.UseCases.Queries;
using WorkflowCatalog.Application.UseCases.Queries.GetUseCaseById;
using WorkflowCatalog.Application.UseCases.Queries.GetUseCases;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class UseCasesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<UseCaseDto>>> GetUseCases([FromQuery] GetUseCasesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UseCaseDto>> GetUseCaseById(Guid id)
        {
            return await Mediator.Send(new GetUseCaseByIdQuery
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUseCase(CreateUseCaseCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUseCaseDetails (Guid id, UpdateUseCaseDetailsCommand command)
        {
            if(id!=command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteUseCase (Guid id, DeleteUseCaseCommand command)
        {
            if(id!=command.Id)
            {
                return BadRequest();
            }
            return await Mediator.Send(command);
        }

        

    }
}
