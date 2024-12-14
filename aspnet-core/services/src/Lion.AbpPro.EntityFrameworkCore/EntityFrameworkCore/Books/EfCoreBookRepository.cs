using Lion.AbpPro.Books;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lion.AbpPro.EntityFrameworkCore.Books;

/// <summary>
/// 书籍 仓储Ef core 实现
/// </summary>
public  class EfCoreBookRepository :
    EfCoreRepository<IAbpProDbContext, Book, Guid>,
    IBookRepository
{
    public EfCoreBookRepository(IDbContextProvider<IAbpProDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Book>> GetListAsync(DateTime? startDateTime = null, DateTime? endDateTime = null,int maxResultCount = 10, int skipCount = 0)
    {
        return await (await GetDbSetAsync())
            .WhereIf(startDateTime.HasValue, e => e.CreationTime >= startDateTime.Value)
            .WhereIf(endDateTime.HasValue, e => e.CreationTime <= endDateTime.Value)
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    public async Task<long> GetCountAsync(DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        return await (await GetDbSetAsync())
               .WhereIf(startDateTime.HasValue, e => e.CreationTime >= startDateTime.Value)
               .WhereIf(endDateTime.HasValue, e => e.CreationTime <= endDateTime.Value)
               .CountAsync();
    }
}