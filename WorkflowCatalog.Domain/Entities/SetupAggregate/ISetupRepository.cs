using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities.SetupAggregate
{
    public interface ISetupRepository : IRepository<Setup>
    {
        Setup Add(Setup setup);
        void Update(Setup setup);
        Task<Setup> FindAsync(string abbreviation);
        Task<Setup> FindAsyncByName(string name);
        Task<Setup> FindAsyncById(int setupId);
    }
}
