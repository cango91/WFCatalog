using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WorkflowCatalog.Application.Common.Interfaces;

namespace WorkflowCatalog.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("userId");
        //public string UserId => "5BE86359-073C-434B-AD2D-A3932222DABE";
    }
}
