namespace Lion.AbpPro.LanguageManagement.LanguageTexts;


/// <summary>
/// 语言文本
/// </summary>
public interface ILanguageTextAppService : IApplicationService
{
    /// <summary>
    /// 获取所有资源
    /// </summary>    
    Task<List<FromSelector<string, string>>> AllResourceListAsync();
    
    /// <summary>
    /// 分页查询语言文本
    /// </summary>
    Task<PagedResultDto<PageLanguageTextOutput>> PageAsync(PageLanguageTextInput input);
    
    /// <summary>
    /// 创建语言文本
    /// </summary>    
    Task CreateAsync(CreateLanguageTextInput input);

    /// <summary>
    /// 编辑语言文本
    /// </summary>
    Task UpdateAsync(UpdateLanguageTextInput input);
}