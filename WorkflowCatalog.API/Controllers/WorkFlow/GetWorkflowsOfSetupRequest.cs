using System;
namespace WorkflowCatalog.API.Controllers.WorkFlow
{
    public class GetWorkflowsOfSetupRequest
    {

        public string sortBy = "id";

        public string order = "asc";

        public int[] filterTypes = null;

        public int page = 1;

        public int pageSize = 5;

        public GetWorkflowsOfSetupRequest()
        {
           
        }
    }
}
