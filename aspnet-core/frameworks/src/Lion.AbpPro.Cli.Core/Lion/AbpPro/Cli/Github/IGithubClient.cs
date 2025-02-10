namespace Lion.AbpPro.Cli.Github;

public interface IGithubClient
{
    /// <summary>
    /// 获取最后一个版本
    /// </summary>
    Task<Release> GetLatestVersionAsync(string owner,string repositoryId,string token);

    /// <summary>
    /// 检查版本是否存在
    /// </summary>
    Task<Release> CheckVersionAsync(string owner, string repositoryId,string token, string version);

    /// <summary>
    /// 下载源码
    /// </summary>
    Task DownloadAsync(string url, string localFilePath, string token);

    /// <summary>
    /// 下载源码
    /// public 仓库使用
    /// </summary>
    Task DownloadAsync(string owner, string repositoryId, string version, string localFilePath);
}