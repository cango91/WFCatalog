using System;
using MediatR;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Setup.Commands.UpdateSetupDetails
{
    public class UpdateSetupDetailsCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public SetupStatus Status { get; set; }
    }
}
