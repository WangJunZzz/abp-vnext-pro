using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lion.AbpPro.FileManagement.EntityFrameworkCore.Files;

/// <summary>
/// 文件 仓储Ef core 实现
/// </summary>
public class EfCoreFileObjectRepository :
    EfCoreRepository<FileManagementDbContext, FileObject, Guid>,
    IFileObjectRepository
{
    public EfCoreFileObjectRepository(IDbContextProvider<FileManagementDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<FileObject>> GetListAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null, int maxResultCount = 10, int skipCount = 0)
    {
        return await (await GetDbSetAsync())
            .WhereIf(fileName.IsNotNullOrWhiteSpace(), e => e.FileName.Contains(fileName))
            .WhereIf(startDateTime.HasValue, e => e.CreationTime >= startDateTime.Value)
            .WhereIf(endDateTime.HasValue, e => e.CreationTime <= endDateTime.Value)
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    public async Task<long> GetCountAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        return await (await GetDbSetAsync())
            .WhereIf(fileName.IsNotNullOrWhiteSpace(), e => e.FileName.Contains(fileName))
            .WhereIf(startDateTime.HasValue, e => e.CreationTime >= startDateTime.Value)
            .WhereIf(endDateTime.HasValue, e => e.CreationTime <= endDateTime.Value)
            .CountAsync();
    }

    public async Task<FileObject> FindAsync(string fileName)
    {
        return await (await GetDbSetAsync()).FirstOrDefaultAsync(e => e.FileName == fileName);
    }
}