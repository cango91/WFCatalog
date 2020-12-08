using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCatalog.Application.UseCases.Queries;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Common
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(
       IOptions<SieveOptions> options,
       ISieveCustomSortMethods customSortMethods,
       ISieveCustomFilterMethods customFilterMethods)
       :  base(options, customFilterMethods)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<UseCase>(uc => uc.Id)
                .CanFilter()
                .CanSort();
            mapper.Property<UseCase>(uc => uc.Name)
                .CanSort()
                .CanFilter();
            mapper.Property<UseCase>(uc => uc.Workflow.Id)
                .CanSort()
                .CanFilter();
            mapper.Property<UseCase>(uc => uc.Description)
                .CanFilter()
                .CanSort();
            mapper.Property<UseCase>(uc => uc.Preconditions)
                .CanSort()
                .CanFilter();
            mapper.Property<UseCase>(uc => uc.Postconditions)
                .CanFilter()
                .CanSort();
            mapper.Property<UseCase>(uc => uc.AltCourse)
                .CanSort()
                .CanFilter();
            mapper.Property<UseCase>(uc => uc.NormalCourse)
                .CanFilter()
                .CanSort();


            mapper.Property<Actor>(a => a.Id)
                .CanFilter()
                .CanSort();
            mapper.Property<Actor>(a => a.Name)
                .CanSort()
                .CanFilter();
            mapper.Property<Actor>(a => a.Description)
                .CanFilter()
                .CanSort();
            mapper.Property<Actor>(a => a.Setup.Id)
                .CanFilter();

            return mapper;
        }
    }
}