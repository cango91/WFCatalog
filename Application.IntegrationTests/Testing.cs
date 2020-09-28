using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using Moq;
using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using WorkflowCatalog.API;
using System.Linq;
using WorkflowCatalog.Application.Common.Interfaces;

[SetUpFixture]
public class Testing
{
    private static IConfigurationRoot _configuration;
    private static IServiceScopeFactory _scopeFactory;
    private static Checkpoint _checkpoint;
    private static string _currentUserId;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();
    
        _configuration = builder.Build();

        var startup = new Startup(_configuration);

        var services = new ServiceCollection();


        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
        w.EnvironmentName == "Development" &&
        w.ApplicationName == "WorkflowCatalog.API"));

        services.AddLogging();

        startup.ConfigureServices(services);

        var currentUserServiceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(ICurrentUserService));

        services.Remove(currentUserServiceDescriptor);

        services.AddTransient(provider => Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));

        
    }

    
}
