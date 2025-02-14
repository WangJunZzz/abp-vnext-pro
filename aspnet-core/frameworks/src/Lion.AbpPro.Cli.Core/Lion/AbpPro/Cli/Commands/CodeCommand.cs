namespace Lion.AbpPro.Cli.Commands;

public class CodeCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "code";
    private readonly ILogger<CodeCommand> _logger;
    private readonly ICodeService _codeService;

    public CodeCommand(ICodeService codeService, ILogger<CodeCommand> logger)
    {
        _codeService = codeService;
        _logger = logger;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        var template = commandLineArgs.Options.GetOrNull(CommandOptions.Template.Short, CommandOptions.Template.Long);
        var project = commandLineArgs.Options.GetOrNull(CommandOptions.Project.Short, CommandOptions.Project.Long);
        var accessToken = await _codeService.GetAccessTokenAsync();
        
        if (Guid.TryParse(template, out Guid templateId) && Guid.TryParse(project, out Guid projectId))
        {
            var path = await _codeService.DownloadAsync(accessToken, projectId, templateId);
            ZipHelper.Extract(path);
        }
        else
        {
            Console.WriteLine("输入的模板 ID 或项目 ID 不是有效的 GUID 格式，请检查后重新输入。");
        }
    }

    public void GetUsageInfo()
    {
    }

    public string GetShortDescription()
    {
        return "生成代码";
    }
}