namespace Lion.AbpPro.LanguageManagement.Languages;

public interface ILanguageRepository : IBasicRepository<Language, Guid>
{
    /// <summary>
    /// 查询语言
    /// </summary>
    Task<List<Language>> ListAsync(bool? isEnabled = null);

    /// <summary>
    /// 查询语言
    /// </summary>
    /// <param name="maxResultCount">返回最大条数</param>
    /// <param name="skipCount">跳过条数</param>
    /// <param name="filter">查询条件 cultureName or uiCultureName or displayName</param>
    Task<List<Language>> ListAsync(int maxResultCount = 10, int skipCount = 0, string filter = null);

    /// <summary>
    /// 获取总条数
    /// </summary>
    /// <param name="filter">查询条件 cultureName or uiCultureName or displayName</param>
    Task<long> CountAsync(string filter = null);

    /// <summary>
    /// 查询指定语言
    /// </summary>
    Task<Language> FindAsync(string cultureName);

    /// <summary>
    /// 获取默认语言
    /// </summary>
    Task<Language> FindDefaultLanguageAsync();
}