using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Enums
{
    public class GetEnumsQuery : IRequest<EnumInfoVm>
    {
    }
    public class GetEnumsQueryHadner : IRequestHandler<GetEnumsQuery,EnumInfoVm>
    {
        public async Task<EnumInfoVm> Handle (GetEnumsQuery query, CancellationToken cancellationToken)
        {
            return new EnumInfoVm
            {
                WorkflowTypes = Enum.GetValues(typeof(WorkflowType))
                .Cast<WorkflowType>()
                .Select(p => new EnumInfoDto { Value = (int)p, Name = p.ToString() })
                .ToList(),
                SetupStatuses = Enum.GetValues(typeof(SetupStatus))
                .Cast<SetupStatus>()
                .Select(p => new EnumInfoDto { Value = (int)p, Name = p.ToString() })
                .ToList()
            };
        }
    }
}
