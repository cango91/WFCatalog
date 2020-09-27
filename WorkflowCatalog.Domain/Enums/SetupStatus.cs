using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using WorkflowCatalog.Domain.Common;

namespace WorkflowCatalog.Domain.Enums
{
    public class SetupStatus : Enumeration
    {
        public static SetupStatus Passive = new SetupStatus(0, nameof(Passive).ToLowerInvariant());
        public static SetupStatus Active = new SetupStatus(1, nameof(Active).ToLowerInvariant());

        public SetupStatus(int id, string name) : base(id,name)
        {
        }

        public static SetupStatus operator !(SetupStatus status)
        {
            return status == SetupStatus.Active ? SetupStatus.Passive : SetupStatus.Active;
        }
    }
}
