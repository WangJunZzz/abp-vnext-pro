namespace Lion.AbpPro.LanguageManagement.EntityFrameworkCore.Languages;

public class EfCoreLanguageRepository :
    EfCoreRepository<ILanguageManagementDbContext, Language, Guid>,
    ILanguageRepository
{
    public EfCoreLanguageRepository(IDbContextProvider<ILanguageManagementDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// 查询语言
    /// </summary>
    public virtual async Task<List<Language>> ListAsync(bool? isEnabled = null)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.CultureName == "zh-Hans" || e.CultureName == "en")
            .WhereIf(isEnabled != null, e => e.IsEnabled == isEnabled)
            .ToListAsync();
    }

    /// <summary>
    /// 查询语言
    /// </summary>
    /// <param name="maxResultCount">返回最大条数</param>
    /// <param name="skipCount">跳过条数</param>
    /// <param name="filter">查询条件 cultureName or uiCultureName or displayName</param>
    public async Task<List<Language>> ListAsync(int maxResultCount = 10, int skipCount = 0, string filter = null)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.CultureName == "zh-Hans" || e.CultureName == "en")
            .WhereIf(filter.IsNotNullOrWhiteSpace(), e => e.CultureName.Contains(filter) || e.UiCultureName.Contains(filter) || e.DisplayName.Contains(filter))
            .OrderByDescending(e => e.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    /// <summary>
    /// 获取总条数
    /// </summary>
    /// <param name="filter">查询条件 cultureName or uiCultureName or displayName</param>
    public async Task<long> CountAsync(string filter = null)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.CultureName == "zh-Hans" || e.CultureName == "en")
            .WhereIf(filter.IsNotNullOrWhiteSpace(), e => e.CultureName.Contains(filter) || e.UiCultureName.Contains(filter) || e.DisplayName.Contains(filter))
            .CountAsync();
    }

    /// <summary>
    /// 查询指定语言
    /// </summary>
    public async Task<Language> FindAsync(string cultureName)
    {
        return await (await GetDbSetAsync())
            .Where(e => e.CultureName == cultureName)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// 获取默认语言
    /// </summary>
    public async Task<Language> FindDefaultLanguageAsync()
    {
        return await (await GetDbSetAsync())
            .Where(e => e.IsDefault)
            .FirstOrDefaultAsync();
    }
}