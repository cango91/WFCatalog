using System;
using static WorkflowCatalog.Application.Common.Interfaces.IDateTimeService;

namespace WorkflowCatalog.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
