namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

public interface ILanguageTextManager
{
    /// <summary>
    /// 查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="filter">筛选条件：name or value</param>
    /// <param name="maxResultCount">返回最大条数</param>
    /// <param name="skipCount">跳过条数</param>
    Task<List<LanguageTextDto>> ListAsync(string cultureName, string resourceName, string filter = null, int maxResultCount = 10, int skipCount = 0);

    /// <summary>
    /// 查询语言文本数量
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="filter">筛选条件：name or value</param>
    Task<long> CountAsync(string cultureName, string resourceName, string filter = null);

    /// <summary>
    /// 创建语言文本
    /// </summary>
    Task<LanguageTextDto> CreateAsync(Guid id, string cultureName, string resourceName, string name, string value);

    /// <summary>
    /// 更新语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="name">键</param> 
    /// <param name="value">值</param> 
    Task<LanguageTextDto> UpdateAsync(string cultureName, string resourceName, string name, string value);

    /// <summary>
    /// 删除语言文本
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 根据资源名称和语言名称查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    Task<List<LanguageTextDto>> FindAsync(string cultureName, string resourceName);
}