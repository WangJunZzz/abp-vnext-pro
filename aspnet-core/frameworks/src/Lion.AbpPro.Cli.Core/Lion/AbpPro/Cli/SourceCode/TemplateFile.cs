namespace Lion.AbpPro.Cli.SourceCode;

public class TemplateFile
{
    public TemplateFile(string version, string sourceCodePath, byte[] fileBytes)
    {
        Version = version;
        SourceCodePath = sourceCodePath;
        FileBytes = fileBytes;
    }

    /// <summary>
    /// 模板文件字节流
    /// </summary>
    public byte[] FileBytes { get; }
    
    /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; }
    
    /// <summary>
    /// 模板zip压缩包地址
    /// </summary>
    public string SourceCodePath { get; }
}