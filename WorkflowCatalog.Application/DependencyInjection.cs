using AutoMapper;
using FileSignatures;
using FileSignatures.Formats;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Models;
using Sieve.Services;
using System.Linq;
using System.Reflection;
using WorkflowCatalog.Application.Common.Behaviors;
using WorkflowCatalog.Application.Diagrams.Commands.CreateDiagram;

namespace WorkflowCatalog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            /*var recognised = FileFormatLocator.GetFormats()
                .OfType<Image>()
                .OfType<Pdf>()
                .OfType<Visio>()
                .OfType<VisioLegacy>();
            var inspector = new FileFormatInspector(recognised);*/

            services.AddSingleton<IFileFormatInspector>(new FileFormatInspector());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<CreateDiagramCommand>,CreateDiagramCommandValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            

            return services;
        }
    }
}
