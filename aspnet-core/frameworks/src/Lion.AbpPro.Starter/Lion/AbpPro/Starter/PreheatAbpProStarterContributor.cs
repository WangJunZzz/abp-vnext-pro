using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lion.AbpPro.Starter;

public class PreheatAbpProStarterContributor : IAbpProStarterContributor, ITransientDependency
{
    private readonly IHttpClientFactory _httpClientFactory;
    private PreheatOptions _options;
    private ILogger<PreheatAbpProStarterContributor> _logger;

    public PreheatAbpProStarterContributor(IHttpClientFactory httpClientFactory, IOptions<PreheatOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task RunAsync()
    {
        if (!_options.Enabled)
        {
            return;
        }

        try
        {
            _logger.LogInformation($"开始预热:{_options.RequestUrl}");
            await _httpClientFactory.CreateClient().GetAsync(_options.RequestUrl);
            _logger.LogInformation($"预热成功");
        }
        catch (Exception ex)
        {
            // ignore
            _logger.LogError(ex, $"程序预热失败:{_options.RequestUrl}");
        }
    }
}