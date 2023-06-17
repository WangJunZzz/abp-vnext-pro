namespace Lion.AbpPro.Cli.Github;

public interface ILionAbpProManager
{
    /// <summary>
    /// 获取最后一个版本
    /// </summary>
    Task<string> GetLatestSourceCodeVersionAsync();

    /// <summary>
    /// 检查版本是否存在
    /// </summary>
    Task<bool> CheckSourceCodeVersionAsync(string version);

    /// <summary>
    /// 下载源码
    /// </summary>
    Task<byte[]> DownloadAsync(string version,string outputPath);
}