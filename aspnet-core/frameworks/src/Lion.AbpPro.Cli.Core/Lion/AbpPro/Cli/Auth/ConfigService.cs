using System.Text.Json;

namespace Lion.AbpPro.Cli.Auth;

public class ConfigService : IConfigService, ITransientDependency
{
    private readonly IJsonSerializer _jsonSerializer;

    public ConfigService(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public async Task SetAsync(string config)
    {
        if (!Directory.Exists(CliPaths.Root))
        {
            Directory.CreateDirectory(CliPaths.Root);
        }

        await File.WriteAllTextAsync(CliPaths.Config, config, Encoding.UTF8);
    }

    public async Task<ConfigOptions> GetAsync()
    {
        if (!File.Exists(CliPaths.Config))
        {
            return new ConfigOptions()
            {
                CodeServiceUrl = "http://182.43.18.151:44317",
                UserName = "admin",
                Password = "1q2w3E*",
                TenantName = string.Empty,
                TenantId = string.Empty
            };
        }

        var content = await File.ReadAllTextAsync(CliPaths.Config);
        return _jsonSerializer.Deserialize<ConfigOptions>(content);
    }
}