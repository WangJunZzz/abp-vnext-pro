using Lion.AbpPro.EntityFrameworkCore.Tests.Blogs;
using Lion.AbpPro.EntityFrameworkCore.Tests.Entities.Blogs;
using Microsoft.EntityFrameworkCore;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Data.Blogs;

public static class EfCoreBlogQueryableExtensions
{
    public static IQueryable<Blog> IncludeDetails(this IQueryable<Blog> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(e => e.Posts)
            .ThenInclude(x => x.Comments);
    }
}