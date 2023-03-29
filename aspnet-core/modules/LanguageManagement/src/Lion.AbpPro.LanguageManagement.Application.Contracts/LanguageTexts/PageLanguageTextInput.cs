namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

/// <summary>
/// 创建语言文本
/// </summary>
public class PageLanguageTextInput : PagingBase
{

    /// <summary>
    /// 语言
    /// </summary>
    public string CultureName { get; set; }
    
    /// <summary>
    /// 资源
    /// </summary>
    public string ResourceName { get; set; }
    
    /// <summary>
    /// 查询条件 name or value
    /// </summary>
    public string Filter { get; set; }

}