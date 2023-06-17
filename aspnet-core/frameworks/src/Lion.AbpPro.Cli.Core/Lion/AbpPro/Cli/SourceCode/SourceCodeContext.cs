namespace Lion.AbpPro.Cli.SourceCode;

public class SourceCodeContext
{
    /// <summary>
    /// 模板信息
    /// </summary>
    public TemplateFile TemplateFile { get; set; }
    
    /// <summary>
    /// 需要替换的文件
    /// </summary>
    public string ReplaceSuffix { get; set; }

    /// <summary>
    /// 需要排除的文件
    /// </summary>
    public string ExcludeFiles { get; set; }
    
    /// <summary>
    /// 替换CompanyName
    /// </summary>
    public string OldCompanyName { get; set; }
    
    /// <summary>
    /// 替换ProjectName
    /// </summary>
    public string OldProjectName { get; set; }
    /// <summary>
    /// New CompanyName
    /// </summary>
    public string CompanyName { get; set; }
    /// <summary>
    /// New ProjectName
    /// </summary>
    public string ProjectName { get; set; }
    /// <summary>
    /// 输入文件夹
    /// </summary>
    public string OutputFolder { get; set; }
    /// <summary>
    /// 模板文件夹
    /// </summary>
    public string TemplateFolder { get; set; }
    
    /// <summary>
    /// 模板key
    /// </summary>
    public string TemplateKey { get; set; }
    
    /// <summary>
    /// 模板名称
    /// </summary>
    public string TemplateName { get; set; }
    
    /// <summary>
    /// 解压目录
    /// </summary>
    public string ExtractProjectPath { get; set; }

    /// <summary>
    /// 是否源码版本
    /// </summary>
    public bool IsSource { get; set; }
    
    /// <summary>
    /// 仓库拥有者
    /// </summary>
    public string Owner { get; set; }
    
    /// <summary>
    /// 仓库Id
    /// </summary>
    public string RepositoryId { get; set; }
    
    /// <summary>
    /// Github Token
    /// </summary>
    public string Token { get; set; }
}