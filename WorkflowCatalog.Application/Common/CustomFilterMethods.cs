using Sieve.Services; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Application.UseCases.Queries;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Common
{

    public class CustomFilterMethods : ISieveCustomFilterMethods
    {
        public IQueryable<UseCase> Actors(IQueryable<UseCase> source, string op, string[] values)
        {
            var ids = values.Select(a => Guid.TryParse(a, out Guid b) ? b : Guid.Empty).ToList();
            return source.Where(x => x.UseCaseActors.Any(y => ids.Any(a => a == y.ActorId)));
        }

        public IQueryable<Actor> UseCases(IQueryable<Actor> source, string op, string[] values)
        {
            var ids = values.Select(a => Guid.TryParse(a, out Guid b) ? b : Guid.Empty).ToList();
            return source.Where(x => x.UseCaseActors.Any(y => ids.Any(a => a == y.UseCaseId)));
        }
    }
}
