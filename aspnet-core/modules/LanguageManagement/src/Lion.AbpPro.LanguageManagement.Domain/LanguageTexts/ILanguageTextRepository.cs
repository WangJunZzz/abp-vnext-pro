namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

public interface ILanguageTextRepository : IBasicRepository<LanguageText, Guid>
{
    /// <summary>
    /// 查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="filter">筛选条件：name or value</param>
    /// <param name="maxResultCount">返回最大条数</param>
    /// <param name="skipCount">跳过条数</param>
    Task<List<LanguageText>> ListAsync(string cultureName, string resourceName, string filter = null, int maxResultCount = 10, int skipCount = 0);

    /// <summary>
    /// 查询语言文本数量
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="filter">筛选条件：name or value</param>
    Task<long> CountAsync(string cultureName, string resourceName, string filter = null);

    /// <summary>
    /// 查询语言文本
    /// </summary>
    Task<List<LanguageText>> FindAsync(string cultureName, string resourceName);
    
    /// <summary>
    /// 查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="name">名称</param> 
    Task<LanguageText> FindAsync(string cultureName, string resourceName, string name);
    
}