using System;
namespace WorkflowCatalog.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        public interface IDateTime
        {
            DateTime Now { get; }
        }
    }
}
