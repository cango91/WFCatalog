using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowCatalog.Application.Enums
{
    public class EnumInfoVm
    {
        public IList<EnumInfoDto> WorkflowTypes { get; set; }
        public IList<EnumInfoDto> SetupStatuses { get; set; }
    }
}
