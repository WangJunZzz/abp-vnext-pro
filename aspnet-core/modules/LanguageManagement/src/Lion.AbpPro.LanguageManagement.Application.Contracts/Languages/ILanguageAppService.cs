namespace Lion.AbpPro.LanguageManagement.Languages;


/// <summary>
/// 语言
/// </summary>
public interface ILanguageAppService : IApplicationService
{

    /// <summary>
    /// 获取所有语言
    /// </summary>     
    Task<List<PageLanguageOutput>> AllListAsync();
    
    /// <summary>
    /// 分页查询语言
    /// </summary>
    Task<PagedResultDto<PageLanguageOutput>> PageAsync(PageLanguageInput input);
    
    /// <summary>
    /// 创建语言
    /// </summary>    
    Task CreateAsync(CreateLanguageInput input);

    /// <summary>
    /// 编辑语言
    /// </summary>
    Task UpdateAsync(UpdateLanguageInput input);

    /// <summary>
    /// 删除语言
    /// </summary>
    Task DeleteAsync(DeleteLanguageInput input);

    /// <summary>
    /// 设置默认语言
    /// </summary>
    Task SetDefaultAsync(IdInput input);
}