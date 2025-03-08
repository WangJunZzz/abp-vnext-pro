using Lion.AbpPro.Cli.Auth;
using Lion.AbpPro.Cli.Dto;
using FileMode = System.IO.FileMode;

namespace Lion.AbpPro.Cli;

public class CodeService : ICodeService, ITransientDependency
{
    private readonly ILogger<CodeService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IConfigService _configService;

    public CodeService(ILogger<CodeService> logger, IHttpClientFactory httpClientFactory, IJsonSerializer jsonSerializer, IConfigService configService)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _jsonSerializer = jsonSerializer;
        _configService = configService;
    }

    private async Task<ConfigOptions> GetConfigOptionsAsync()
    {
        return await _configService.GetAsync();
    }

    private async Task<HttpClient> GetHttpClientAsync()
    {
        var options = await GetConfigOptionsAsync();
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(options.CodeServiceUrl);
        return httpClient;
    }

    public async Task<string> LoginAsync(string url, string userName, string password)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(url);
        var data = new
        {
            name = userName,
            password = password
        };

        var content = new StringContent(_jsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/app/account/login", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new UserFriendlyException($"用户或者密码错误");
        }

        return _jsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync()).Token;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var options = await GetConfigOptionsAsync();
        using var httpClient = await GetHttpClientAsync();
        var data = new
        {
            name = options.UserName,
            password = options.Password
        };
        httpClient.DefaultRequestHeaders.Add("__tenant", $"{options.TenantId}");
        var content = new StringContent(_jsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/app/account/login", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new UserFriendlyException($"用户或者密码错误");
        }

        return _jsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync()).Token;
    }

    public async Task<string> DownloadAsync(string accessToken, Guid projectId, Guid templateId, List<Guid> entityId)
    {
        var options = await GetConfigOptionsAsync();
        var path = Path.Combine(CliPaths.Source, $"{templateId}_{projectId}.zip");
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        using var httpClient = await GetHttpClientAsync();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        httpClient.DefaultRequestHeaders.Add("__tenant", $"{options.TenantId}");
        var data = new
        {
            templateId = templateId,
            projectId = projectId,
            entityId = entityId
        };

        var content = new StringContent(_jsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/Generator/Down", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new UserFriendlyException($"下载代码失败,状态码{response.StatusCode}");
        }

        // 保存到本地
        await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        await response.Content.CopyToAsync(fileStream);
        return path;
    }

    public async Task<GetProjectAndEntityResponse> GetProjectAndEntityAsync(string accessToken, Guid projectId)
    {
        var options = await GetConfigOptionsAsync();
        using var httpClient = await GetHttpClientAsync();
        var data = new
        {
            id = projectId,
        };
        var content = new StringContent(_jsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        httpClient.DefaultRequestHeaders.Add("__tenant", $"{options.TenantId}");
        var response = await httpClient.PostAsync("/Projects/GetProjectAndEntity", content);
        if (!response.IsSuccessStatusCode)
        {
            var error = _jsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync());
            throw new UserFriendlyException(error.error.message);
        }

        return _jsonSerializer.Deserialize<GetProjectAndEntityResponse>(await response.Content.ReadAsStringAsync());
    }

    public async Task<FindTenantResponse> FindTenantAsync(string url, string tenantName)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = GetUri(url);
        var data = new
        {
            name = tenantName,
        };
        var content = new StringContent(_jsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("Tenants/find", content);
        if (!response.IsSuccessStatusCode)
        {
            var error = _jsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync());
            throw new UserFriendlyException(error.error.message);
        }


        var result = _jsonSerializer.Deserialize<FindTenantResponse>(await response.Content.ReadAsStringAsync());
        if (!result.Success)
        {
            throw new UserFriendlyException($"租户不存在:{tenantName}");
        }

        return result;
    }

    public async Task CheckHealthAsync(string url)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = GetUri(url);
        var response = await httpClient.GetAsync("/health");
        if (!response.IsSuccessStatusCode)
        {
            throw new UserFriendlyException($"服务异常:{url}");
        }
    }

    private Uri GetUri(string url)
    {
        Uri uri;
        try
        {
            uri = new Uri(url);
        }
        catch
        {
            throw new UserFriendlyException($"url格式错误:{url}");
        }

        return uri;
    }
}