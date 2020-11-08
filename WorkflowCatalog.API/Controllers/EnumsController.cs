using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Enums;

namespace WorkflowCatalog.API.Controllers
{
    [Authorize]
    public class EnumsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<EnumInfoVm>> GetEnumsInfo()
        {
            return await Mediator.Send(new GetEnumsQuery());
        }

    }
}
