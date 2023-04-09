namespace Lion.AbpPro.LanguageManagement.Languages;

public interface ILanguageManager
{
    /// <summary>
    /// 查询语言
    /// </summary>
    Task<List<Language>> ListAsync(bool? isEnabled = null);

    /// <summary>
    /// 查询语言
    /// </summary>
    Task<List<LanguageDto>> GetListAsync(int maxResultCount = 10, int skipCount = 0, string filter = null);

    /// <summary>
    /// 获取总条数
    /// </summary>
    /// <param name="filter">查询条件 cultureName or uiCultureName or displayName</param>
    Task<long> CountAsync(string filter = null);

    /// <summary>
    /// 创建语言
    /// </summary>
    Task<LanguageDto> CreateAsync(
        Guid id,
        string cultureName,
        string uiCultureName,
        string displayName,
        string flagIcon,
        bool isEnabled
    );

    /// <summary>
    /// 更新语言
    /// </summary>
    Task<LanguageDto> UpdateAsync(
        Guid id,
        string cultureName,
        string uiCultureName,
        string displayName,
        string flagIcon,
        bool isEnabled
    );

    /// <summary>
    /// 删除语言
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 通过Id获取语言
    /// </summary>
    Task<LanguageDto> GetAsync(Guid id);

    /// <summary>
    /// 获取指定语言
    /// </summary>
    Task<LanguageDto> GetAsync(string cultureName);

    /// <summary>
    /// 设置默认语言
    /// </summary>
    Task SetDefaultAsync(Guid id);
}