using Lion.AbpPro.Cli.Dto;
using FileMode = System.IO.FileMode;

namespace Lion.AbpPro.Cli;

public class CodeService : ICodeService, ITransientDependency
{
    private readonly ILogger<CodeService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonSerializer _jsonSerializer;

    public CodeService(ILogger<CodeService> logger, IHttpClientFactory httpClientFactory, IJsonSerializer jsonSerializer)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri("http://182.43.18.151:44317/");
        var data = new
        {
            name = "admin",
            password = "1q2w3E*"
        };

        var content = new StringContent(_jsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/app/account/login", content);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation($"登录代码生成器服务成功");
        }
        else
        {
            _logger.LogError($"登录代码生成器服务失败,状态码{response.StatusCode}");
        }

        return _jsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync()).Token;
    }

    public async Task<string> DownloadAsync(string accessToken, Guid projectId, Guid templateId)
    {
        var path = Path.Combine(CliPaths.Source, $"{templateId}_{projectId}.zip");
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri("http://182.43.18.151:44317/");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        // 3a179906-5c79-b1e8-69f6-872afc276592
        // 3a18123b-4ac4-b8e7-ddb8-73ac01179aff
        var data = new
        {
            templateId = templateId,
            projectId = projectId
        };

        var content = new StringContent(_jsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/Generator/Down", content);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation($"下载代码成功");
        }
        else
        {
            _logger.LogError($"下载代码失败,状态码{response.StatusCode}");
        }

        // 保存到本地
        await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        await response.Content.CopyToAsync(fileStream);
        _logger.LogInformation($"保存代码成功");
        return path;
    }
}