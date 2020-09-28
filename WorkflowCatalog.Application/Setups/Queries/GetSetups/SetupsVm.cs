using System.Collections.Generic;

namespace WorkflowCatalog.Application.Setups.Queries.GetSetups
{
    public class SetupsVm
    {
       
        public IList<SetupStatusDto> SetupStatus { get; set; }
        public IList<SetupsDto> Setups { get; set; }
        
    }
}
