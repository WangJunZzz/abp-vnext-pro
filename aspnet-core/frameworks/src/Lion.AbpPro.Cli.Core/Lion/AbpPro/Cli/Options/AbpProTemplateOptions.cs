namespace Lion.AbpPro.Cli.Options;

public class AbpProTemplateOptions
{
    public AbpProTemplateOptions(string key, string name, string description, bool isSource = false)
    {
        Key = key;
        Name = name;
        Description = description;
        IsSource = isSource;
    }

    /// <summary>
    /// 模板key
    /// 对应templates下文件夹名称
    /// </summary>
    public string Key { get; set; }
    
    /// <summary>
    /// cli -t  对应参数
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 需要替换的文件
    /// </summary>
    public string ReplaceSuffix { get; set; }

    /// <summary>
    /// 需要排除的文件
    /// </summary>
    public string ExcludeFiles { get; set; }
    
    /// <summary>
    /// 是否源码版本
    /// </summary>
    public bool IsSource { get; set; }


    public string OldCompanyName { get; set; } = string.Empty;

    public string OldProjectName { get; set; } = string.Empty;
    
    public string OldModuleName { get; set; } = string.Empty;
}