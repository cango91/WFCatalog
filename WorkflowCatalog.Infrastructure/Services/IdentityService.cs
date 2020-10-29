using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCatalog.Application.Common.Interfaces;
using WorkflowCatalog.Application.Common.Models;

namespace WorkflowCatalog.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            return Task.FromResult((Result.Success(), "asd"));
        }

        public Task<Result> DeleteUserAsync(string userId)
        {
            return Task.FromResult(Result.Failure(new List<string>()));
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            return Task.FromResult("My Name");
        }
    }
}
