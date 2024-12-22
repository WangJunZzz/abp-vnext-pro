namespace Lion.AbpPro.Cli.SourceCode;

public interface ISourceCodeManager
{
    /// <summary>
    /// 获取源码
    /// </summary>
    /// <param name="version">版本</param>
    Task<TemplateFile> GetAsync(string version);

    /// <summary>
    /// 解压
    /// </summary>
    void ExtractProjectZip(SourceCodeContext context);

    /// <summary>
    /// 替换
    /// </summary>
    void ReplaceTemplates(SourceCodeContext context);

    void ReplaceLocalTemplates(SourceCodeContext context);
}