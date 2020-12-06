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


            //mapper.Property < UseCase >(uc => uc.)



            return mapper;
        }
    }
}

/*[Sieve(CanFilter = true, CanSort = false)]
public Guid Id { get; set; }
[Sieve(CanFilter = true, CanSort = true)]
public Guid WorkflowId { get; set; }
[Sieve(CanFilter = true, CanSort = true)]
public string Name { get; set; }
[Sieve(CanFilter = true, CanSort = true)]
public string Description { get; set; }
public List<UCActorDto> Actors { get; set; }
[Sieve(CanFilter = true, CanSort = true)]
public string Preconditions { get; set; }
[Sieve(CanFilter = true, CanSort = true)]
public string Postconditions { get; set; }
[Sieve(CanFilter = true, CanSort = true)]
public string NormalCourse { get; set; }
[Sieve(CanFilter = true, CanSort = true)]
public string AltCourse { get; set; }*/