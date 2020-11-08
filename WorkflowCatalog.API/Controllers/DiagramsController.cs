using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.Diagrams.Commands.CreateDiagram;
using WorkflowCatalog.Application.Diagrams.Commands.DeleteDiagram;
using WorkflowCatalog.Application.Diagrams.Queries.GetDiagramById;
using WorkflowCatalog.Application.Diagrams.Queries.GetDiagramsMetaData;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class DiagramsController : ApiController
    {
        [HttpPost("forWorkflow/{workflowId}")]
        public async Task<ActionResult<Guid>> CreateDiagram(Guid workflowId, IFormFile file)
        {
            if(file == null)
            {
                return BadRequest();
            }
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return await Mediator.Send(new CreateDiagramCommand
                {
                    File = fileBytes,
                    ContentType = file.ContentType,
                    Name = file.FileName,
                    WorkflowId = workflowId
                });

            }
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<DiagramsMetaDto>>> GetDiagramsMetaData([FromQuery] GetDiagramsMetaQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{diagramId}")]
        public async Task<IActionResult> GetDiagramById(Guid diagramId)
        {
            var dto = await Mediator.Send(new GetDiagramByIdQuery() { Id = diagramId });
            return new FileContentResult(dto.File, dto.ContentType) { FileDownloadName = dto.Name };
        }
        [HttpDelete("{diagramId}")]
        public async Task<ActionResult<Unit>> DeleteDiagram(Guid diagramId,DeleteDiagramCommand command)
        {
            if(diagramId != command.Id)
            {
                return BadRequest();
            }
            return await Mediator.Send(command);
        }
    }
}
