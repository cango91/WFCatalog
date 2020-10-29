using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using WorkflowCatalog.Application.Common.Interfaces;


namespace WorkflowCatalog.Application.Common.Behaviors
{
    class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }
        
       
        



        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;
            //var userId = "5BE86359-073C-434B-AD2D-A3932222DABE";
            //var userName = "cango";

            
            
            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _identityService.GetUserNameAsync(userId);
            }
            
            
            //Console.WriteLine(userName);

            _logger.LogInformation("WFCatalog Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }

    }
}
