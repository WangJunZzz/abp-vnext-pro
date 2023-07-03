namespace Lion.AbpPro.Cli.NuGet;

public class NuGetService : DomainService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IJsonSerializer _jsonSerializer;

    public NuGetService(IHttpClientFactory clientFactory, IJsonSerializer jsonSerializer)
    {
        _clientFactory = clientFactory;
        _jsonSerializer = jsonSerializer;
    }

    /// <summary>
    /// 从nuget服务获取最新的版本
    /// </summary>
    /// <param name="packageId">nuget包</param>
    /// <returns></returns>
    public async Task<string> GetLatestVersionOrNullAsync(string packageId)
    {
        var versionList = await GetPackageVersionsFromNuGetOrgAsync(packageId);
        return versionList.MaxBy(e => e);
    }

    private async Task<List<string>> GetPackageVersionsFromNuGetOrgAsync(string packageId)
    {
        var url = $"https://api.nuget.org/v3-flatcontainer/{packageId.ToLowerInvariant()}/index.json";
        return await GetPackageVersionListFromUrlWithRetryAsync(url);
    }


    private async Task<List<string>> GetPackageVersionListFromUrlWithRetryAsync(string url)
    {
        var exceptionRetryPolicy = CreateExceptionRetryPolicy();
        var timeoutRetryPolicy = CreateTimeoutRetryPolicy();
        var policy = Policy.WrapAsync(exceptionRetryPolicy,timeoutRetryPolicy);

        var result = await policy.ExecuteAsync(async () => await GetPackageVersionListFromUrlAsync(url));
        return result;
    }

    private async Task<List<string>> GetPackageVersionListFromUrlAsync(string url)
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        if (content.IsNullOrWhiteSpace()) return null;
        return _jsonSerializer.Deserialize<NuGetVersionResultDto>(content).Versions;
    }

    /// <summary>
    /// 创建异常重试策略
    /// </summary>
    private AsyncRetryPolicy CreateExceptionRetryPolicy()
    {
        var policy = Policy.Handle<Exception>((ex) =>
            {
                var result = !ex.Message.IsNullOrWhiteSpace();
                return result;
            })
            .WaitAndRetryAsync(3,
                (retryCount) => TimeSpan.FromSeconds(Math.Pow(2, retryCount)), (ex, time, retryCount, context) =>
                {
                    if (retryCount == 3)
                    {
                        Logger.LogError($"请求nuget.org失败,已重试第 {retryCount} 次.");
                    }
                });

        return policy;
    }

    /// <summary>
    /// 创建超时重试策略
    /// </summary>
    private AsyncRetryPolicy CreateTimeoutRetryPolicy()
    {
        var timeOutPolicy = Policy.Handle<Exception>((ex) =>
            {
                var result =
                    ex.InnerException != null &&
                    ex.InnerException.GetType() == typeof(TimeoutException);
                return result;
            })
            .WaitAndRetryAsync(3,
                (retryCount) => TimeSpan.FromSeconds(Math.Pow(2, retryCount)),
                (ex, time, retryCount, context) =>
                {
                    if (retryCount == 3)
                    {
                        Logger.LogError($"请求nuget.org超时，已重试第 {retryCount} 次,请重新执行命令.");
                    }
                });
        return timeOutPolicy;
    }
}

public class NuGetVersionResultDto
{
     public List<string> Versions { get; set; }
}