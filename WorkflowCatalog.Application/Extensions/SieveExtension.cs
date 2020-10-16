using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using WorkflowCatalog.Application.Common.Models;

namespace WorkflowCatalog.Application.Extensions
{
    public static class SieveExtension
    {
       public async static Task<PaginatedList<T>> Paginate<T>(this IQueryable<T> query,SieveProcessor processor,SieveModel model)
        {
            long totalRecords;
            var q = processor.Apply(model, query, applyPagination: false);
            totalRecords = await q.LongCountAsync();

            q = processor.Apply(model, q, applyFiltering: false, applySorting: false);

            return new PaginatedList<T>(await q.ToListAsync(), totalRecords, model.Page ?? 0, model.PageSize ?? 10);
        }

        public async static Task<IList<T>> FilterAndSort<T> (this IQueryable<T> query, SieveProcessor processor, SieveModel model)
        {
            var q = processor.Apply(model, query, applyPagination: false);
            return await q.ToListAsync();
        }
    }
}
