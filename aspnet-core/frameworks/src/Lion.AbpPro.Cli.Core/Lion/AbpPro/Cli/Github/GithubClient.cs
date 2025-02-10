using FileMode = System.IO.FileMode;

namespace Lion.AbpPro.Cli.Github;

public class GithubClient : IGithubClient, ITransientDependency
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GithubClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected virtual GitHubClient GetGitHubClient(string repositoryId, string token)
    {
        return new GitHubClient(new ProductHeaderValue("OctokitApp"))
        {
            // 匿名访问，api会限流，所以需要设置访问令牌
            Credentials = new Credentials(token)
        };
    }

    public async Task<Release> GetLatestVersionAsync(string owner, string repositoryId, string token)
    {
        var github = GetGitHubClient(repositoryId, token);
        var release = await github.Repository.Release.GetLatest(owner, repositoryId);
        if (release == null)
        {
            throw new Exception($"没有找到Release,请联系仓库管理员.");
        }

        return release;
    }

    public async Task<Release> CheckVersionAsync(string owner, string repositoryId, string token, string version)
    {
        var github = GetGitHubClient(repositoryId, token);
        var release = await github.Repository.Release.Get(owner, repositoryId, version);
        if (release == null)
        {
            throw new UserFriendlyException($"版本{version}不存在.");
        }

        return release;
    }

    public async Task DownloadAsync(string url, string localFilePath, string token)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        // 添加 GitHub 认证头
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("OctokitApp");
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

        // 下载文件
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        // 保存到本地
        await using var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write);
        await response.Content.CopyToAsync(fileStream);
    }

    /// <summary>
    /// 下载源码
    /// </summary>
    public async Task DownloadAsync(string owner, string repositoryId, string version, string localFilePath)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var uri = new Uri($"https://github.com/{owner}/{repositoryId}/archive/refs/tags/{version}.zip");
        var response = await httpClient.GetAsync(uri);
        // 保存到本地
        await using var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write);
        await response.Content.CopyToAsync(fileStream);
    }
}