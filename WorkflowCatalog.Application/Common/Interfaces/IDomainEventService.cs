using System.Threading.Tasks;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
