using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkflowCatalog.Application.Diagrams.Commands.CreateDiagram;
using WorkflowCatalog.Application.Diagrams.Queries.GetDiagram;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class DiagramsController : ApiController
    {
        [HttpPost("{workFlowId}")]
        public async Task<int> AddDiagram(int workFlowId, IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return await Mediator.Send(new CreateDiagramCommand
                {
                    File = fileBytes,
                    MimeType = file.ContentType,
                    Name = file.FileName,
                    WorkflowId = workFlowId
                });

            }
        }

        [HttpGet("{workFlowId}")]
        public async Task<IActionResult> Get(int workFlowId)
        {
            var dto = await Mediator.Send(new GetDiagramQuery() { Id = workFlowId });


            return new FileContentResult(dto.File, dto.MimeType) { FileDownloadName = dto.Name };
        }


    }
}
