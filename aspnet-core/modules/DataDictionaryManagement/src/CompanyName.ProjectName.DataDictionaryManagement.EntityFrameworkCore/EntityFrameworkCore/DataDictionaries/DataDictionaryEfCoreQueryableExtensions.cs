using System.Linq;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries
{
    public static class DataDictionaryEfCoreQueryableExtensions
    {
        public static IQueryable<DataDictionary> IncludeDetails(this IQueryable<DataDictionary> queryable, 
            bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable.Include(x => x.Details);
        }
    }
}