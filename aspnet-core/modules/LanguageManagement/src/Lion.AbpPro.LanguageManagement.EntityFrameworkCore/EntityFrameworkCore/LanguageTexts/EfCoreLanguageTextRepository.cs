namespace Lion.AbpPro.LanguageManagement.EntityFrameworkCore.LanguageTexts;

public class EfCoreLanguageTextRepository :
    EfCoreRepository<ILanguageManagementDbContext, LanguageText, Guid>,
    ILanguageTextRepository
{
    public EfCoreLanguageTextRepository(IDbContextProvider<ILanguageManagementDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// 查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="filter">筛选条件：name or value</param>
    /// <param name="maxResultCount">返回最大条数</param>
    /// <param name="skipCount">跳过条数</param>
    public async Task<List<LanguageText>> ListAsync(string cultureName, string resourceName, string filter = null, int maxResultCount = 10, int skipCount = 0)
    {
        return await (await GetDbSetAsync())
            .WhereIf(cultureName.IsNotNullOrWhiteSpace(), e => e.CultureName == cultureName)
            .WhereIf(resourceName.IsNotNullOrWhiteSpace(), e => e.ResourceName == resourceName)
            .WhereIf(filter.IsNotNullOrWhiteSpace(), e => e.Name.Contains(filter) || e.Value.Contains(filter))
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    /// <summary>
    /// 查询语言文本数量
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="filter">筛选条件：name or value</param>
    public async Task<long> CountAsync(string cultureName, string resourceName, string filter = null)
    {
        return await (await GetDbSetAsync())
            .WhereIf(cultureName.IsNotNullOrWhiteSpace(), e => e.CultureName == cultureName)
            .WhereIf(resourceName.IsNotNullOrWhiteSpace(), e => e.ResourceName == resourceName)
            .WhereIf(filter.IsNotNullOrWhiteSpace(), e => e.Name.Contains(filter) || e.Value.Contains(filter))
            .CountAsync();
    }

    /// <summary>
    /// 查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    public async Task<List<LanguageText>> FindAsync(string cultureName, string resourceName)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.CultureName == cultureName)
            .Where(e => e.ResourceName == resourceName)
            .ToListAsync();
    }

    /// <summary>
    /// 查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    public async Task<LanguageText> FindOneAsync(string cultureName, string resourceName)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.CultureName == cultureName)
            .Where(e => e.ResourceName == resourceName)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// 查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="name">名称</param>
    public async Task<LanguageText> FindAsync(string cultureName, string resourceName, string name)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.CultureName == cultureName)
            .Where(e => e.ResourceName == resourceName)
            .Where(e => e.Name == name)
            .FirstOrDefaultAsync();
    }
}