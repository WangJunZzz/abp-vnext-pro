namespace Lion.AbpPro.FileManagement.EntityFrameworkCore.Files;

public class EfCoreFileRepository: EfCoreRepository<IFileManagementDbContext,Lion.AbpPro.FileManagement.Files.File, Guid>, IFileRepository
{
    public EfCoreFileRepository(IDbContextProvider<IFileManagementDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    
    public async Task<List<Lion.AbpPro.FileManagement.Files.File>> GetPagingListAsync(
        string filter = null,
        int maxResultCount = 10,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .WhereIf(!filter.IsNullOrWhiteSpace(),
                e => (e.FileName.Contains(filter)))
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<long> GetPagingCountAsync(string filter = null,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .WhereIf(!filter.IsNullOrWhiteSpace(),
                e => (e.FileName.Contains(filter)))
            .CountAsync(cancellationToken: cancellationToken);
    }
}