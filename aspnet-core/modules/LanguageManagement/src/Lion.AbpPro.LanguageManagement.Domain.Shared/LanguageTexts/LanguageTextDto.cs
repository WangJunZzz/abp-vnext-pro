namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

public class LanguageTextDto
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 资源名称
    /// </summary>
    public string ResourceName { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string CultureName { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }

}