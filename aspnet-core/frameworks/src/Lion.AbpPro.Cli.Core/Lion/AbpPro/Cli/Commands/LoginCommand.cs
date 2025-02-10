using Lion.AbpPro.Cli.Auth;

namespace Lion.AbpPro.Cli.Commands;

public class LoginCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "login";
    private readonly ILogger<LoginCommand> _logger;
    private readonly ITokenAuthService _tokenAuthService;

    public LoginCommand(ILogger<LoginCommand> logger, ITokenAuthService tokenAuthService)
    {
        _logger = logger;
        _tokenAuthService = tokenAuthService;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        // 获取参数
        var token = commandLineArgs.Options.GetOrNull(CommandOptions.Token.Short, CommandOptions.Token.Long);
        if (token.IsNullOrWhiteSpace())
        {
            _logger.LogError("请输入token,完成命令:lion.abp login -token 你的token");
        }

        // 保存token
        await _tokenAuthService.SetAsync(token);
        _logger.LogInformation("恭喜你设置token成功.");
    }

    public void GetUsageInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  lion.abp login");
        sb.AppendLine("");
        sb.AppendLine("请在github上创建token(经典),然后当作此命令的参数.");
        _logger.LogInformation(sb.ToString());
    }

    public string GetShortDescription()
    {
        return "登录: lion.abp login -token 你的token(请联系管理员)";
    }
}