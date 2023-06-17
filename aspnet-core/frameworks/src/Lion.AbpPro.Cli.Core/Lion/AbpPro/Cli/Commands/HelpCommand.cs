namespace Lion.AbpPro.Cli.Commands;

public class HelpCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "help";
    private readonly ILogger<HelpCommand> _logger;
    private readonly AbpCliOptions _abpCliOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HelpCommand(IOptions<AbpCliOptions> abpCliOptions,
        ILogger<HelpCommand> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _abpCliOptions = abpCliOptions.Value;
    }

    public Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        GetUsageInfo();
        return Task.CompletedTask;
    }

    public void GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("查看命令帮助:");
        sb.AppendLine("    lion.abp help");
        sb.AppendLine("命令列表:");

        foreach (var command in _abpCliOptions.Commands.ToArray())
        {
            string shortDescription;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                shortDescription = ((IConsoleCommand)scope.ServiceProvider
                    .GetRequiredService(command.Value)).GetShortDescription();
            }

            sb.Append("    > ");
            sb.Append(command.Key);
            sb.Append(string.IsNullOrWhiteSpace(shortDescription) ? "" : ":");
            sb.Append(" ");
            sb.AppendLine(shortDescription);
        }

        _logger.LogInformation(sb.ToString());
    }

    public string GetShortDescription()
    {
        return "lion.abp help";
    }
}