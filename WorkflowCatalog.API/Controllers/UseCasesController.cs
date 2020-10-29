using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.UseCases.Commands.CreateUseCase;

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

    }
}
