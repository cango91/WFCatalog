using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Diagrams.Queries.DumpDiagramsInfo;
using WorkflowCatalog.Application.Setups.Queries.DumpSetups;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Application.UCActors.Queries.DumpActors;
using WorkflowCatalog.Application.UseCases.Queries.DumpUseCases;
using WorkflowCatalog.Application.Workflows.Queries.DumpWorkflows;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class DumpController : ApiController
    {
        [HttpGet("Setups")]
        public async Task<ActionResult<SetupsDumpVm>> DumpSetups([FromQuery] DumpSetupsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("Workflows")]
        public async Task<ActionResult<WorkflowsDumpVm>> DumpWorkflows([FromQuery] DumpWorkflowsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("UseCases")]
        public async Task<ActionResult<PaginatedList<UseCasesDumpDto>>> DumpUseCases([FromQuery] DumpUseCasesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("Actors")]
        public async Task<ActionResult<PaginatedList<UCActorDto>>> DumpActors([FromQuery] DumpActorsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("Diagrams")]
        public async Task<ActionResult<PaginatedList<DiagramsInfoDto>>> DumpDiagramsInfo([FromQuery] DumpDiagramsInfoQuery query)
        {
            return await Mediator.Send(query);
        }


    }
}
