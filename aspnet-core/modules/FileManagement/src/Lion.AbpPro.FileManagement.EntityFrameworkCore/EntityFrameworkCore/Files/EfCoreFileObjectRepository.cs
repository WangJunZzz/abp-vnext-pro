using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lion.AbpPro.FileManagement.EntityFrameworkCore.Files;

/// <summary>
/// 文件 仓储Ef core 实现
/// </summary>
public  class EfCoreFileObjectRepository :
    EfCoreRepository<FileManagementDbContext, FileObject, Guid>,
    IFileObjectRepository
{
    public EfCoreFileObjectRepository(IDbContextProvider<FileManagementDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<FileObjectDto>> GetListAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null,int maxResultCount = 10, int skipCount = 0)
    {
        return await (await GetDbSetAsync())
            .WhereIf(fileName.IsNotNullOrWhiteSpace(),e=>e.FileName.Contains(fileName))
            .WhereIf(startDateTime.HasValue, e => e.CreationTime >= startDateTime.Value)
            .WhereIf(endDateTime.HasValue, e => e.CreationTime <= endDateTime.Value)
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .Select(e => new FileObjectDto
            {
                
                Id = e.Id,
                CreationTime = e.CreationTime,
                FileName =  e.FileName,
                FileSize = e.FileSize,
                FileExtension = e.FileExtension,
                ContentType = e.ContentType,
                ProviderKey = e.ProviderKey
            }) // 选择指定字段
            .ToListAsync();
    }

    public async Task<long> GetCountAsync(string fileName, DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        return await (await GetDbSetAsync())
            .WhereIf(fileName.IsNotNullOrWhiteSpace(),e=>e.FileName.Contains(fileName))
            .WhereIf(startDateTime.HasValue, e => e.CreationTime >= startDateTime.Value)
            .WhereIf(endDateTime.HasValue, e => e.CreationTime <= endDateTime.Value)
            .CountAsync();
    }
}