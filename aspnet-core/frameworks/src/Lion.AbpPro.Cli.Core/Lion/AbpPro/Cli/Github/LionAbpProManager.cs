using Uri = System.Uri;

namespace Lion.AbpPro.Cli.Github;

public class LionAbpProManager : ITransientDependency, ILionAbpProManager
{
    private readonly LionAbpProOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;

    public LionAbpProManager(IOptions<LionAbpProOptions> options, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    /// <summary>
    /// 获取最后一个版本
    /// </summary>
    public async Task<string> GetLatestSourceCodeVersionAsync()
    {
        var github = new GitHubClient(new ProductHeaderValue(_options.RepositoryId))
        {
            // 匿名访问，api会限流，所以需要设置访问令牌
            Credentials = new Credentials(_options.DecryptToken)
        };

        var release = await github.Repository.Release.GetLatest(_options.Owner, _options.RepositoryId);
        return release?.TagName;
    }

    /// <summary>
    /// 检查版本是否存在
    /// </summary>
    public async Task<bool> CheckSourceCodeVersionAsync(string version)
    {
        try
        {
            var github = new GitHubClient(new ProductHeaderValue(_options.RepositoryId))
            {
                // 匿名访问，api会限流，所以需要设置访问令牌
                Credentials = new Credentials(_options.DecryptToken)
            };

            var release = await github.Repository.Release.Get(_options.Owner, _options.RepositoryId, version);
            return release != null;
        }
        catch
        {
            return false;
        }
    }
  
    /// <summary>
    /// 下载源码
    /// </summary>
    public async Task<byte[]> DownloadAsync(string version, string outputPath)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var uri = new Uri($"https://github.com/{_options.Owner}/{_options.RepositoryId}/archive/refs/tags/{version}.zip");
        var response = await httpClient.GetAsync(uri);
        DirectoryHelper.CreateIfNotExists(CliPaths.TemplateCache);
        var content = await response.Content.ReadAsByteArrayAsync();
        response.Dispose();
        File.Delete(outputPath);
        await File.WriteAllBytesAsync(outputPath, content);
        return content;
    }
}