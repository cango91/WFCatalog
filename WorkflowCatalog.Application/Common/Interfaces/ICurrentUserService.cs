using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCatalog.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
