using Lion.AbpPro.Cli.Auth;

namespace Lion.AbpPro.Cli.Commands;

public class ConfigCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "config";
    private readonly ILogger<ConfigCommand> _logger;
    private readonly IConfigService _configService;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ICodeService _codeService;

    public ConfigCommand(ILogger<ConfigCommand> logger, IConfigService configService, IJsonSerializer jsonSerializer, ICodeService codeService)
    {
        _logger = logger;
        _configService = configService;
        _jsonSerializer = jsonSerializer;
        _codeService = codeService;
    }

    /// <summary>
    /// config -c http://182.43.18.151:44317 -u admin -p 1q2w3E* -t ss
    /// </summary>
    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        // 获取参数
        var url = commandLineArgs.Options.GetOrNull(CommandOptions.CodeServiceUrl.Short, CommandOptions.CodeServiceUrl.Long);
        var userName = commandLineArgs.Options.GetOrNull(CommandOptions.UserName.Short, CommandOptions.UserName.Long);
        var password = commandLineArgs.Options.GetOrNull(CommandOptions.Password.Short, CommandOptions.Password.Long);
        var tenantName = commandLineArgs.Options.GetOrNull(CommandOptions.TenantName.Short, CommandOptions.TenantName.Long);

        //1 判断url是否有效
        await _codeService.CheckHealthAsync(url);

        var tenantId = string.Empty;
        //2 判断租户是否存在
        if (!tenantName.IsNullOrWhiteSpace())
        {
            var tenant = await _codeService.FindTenantAsync(url, tenantName);
            tenantId = tenant.TenantId.ToString();
        }

        //3. 判断用户是否存在
        await _codeService.LoginAsync(url, userName, password);

        var content = new ConfigOptions()
        {
            CodeServiceUrl = url,
            UserName = userName,
            Password = password,
            TenantName = tenantName,
            TenantId = tenantId
        };
        await _configService.SetAsync(_jsonSerializer.Serialize(content));
        AnsiConsole.MarkupLine("[green]恭喜你,恭喜你设置config成功![/]");
    }

    public void GetUsageInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  lion.abp config");
        sb.AppendLine("");
        sb.AppendLine("配置: lion.abp config");
        _logger.LogInformation(sb.ToString());
    }

    public string GetShortDescription()
    {
        return "配置: lion.abp config -c http://182.43.18.151:44317 -u admin -p 1q2w3E* -t test";
    }
}