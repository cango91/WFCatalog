using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Entities.SetupAggregate
{
    public interface ISetupRepository : IRepository<Setup>
    {
        Setup CreateSetup(Setup setup);
        void UpdateSetup(int setupId, Setup setup);
        void RemoveSetup(Setup setup);
        Workflow AddWorkflow(int wfId, Workflow wf);
        void UpdateWorkflow(int wfId, Workflow wf);
        void RemoveWorkflow(int wfId);
        UseCase AddUseCase(int wfId, UseCase useCase);
        void UpdateUseCase(int useCaseId, UseCase useCase);
        void RemoveUseCase(int useCaseId);



        Task<Setup> FindSetupAsync(string abbreviation);
        Task<Setup> FindSetupAsyncByName(string name);
        Task<Setup> FindSetupAsyncById(int setupId);
    }
}
