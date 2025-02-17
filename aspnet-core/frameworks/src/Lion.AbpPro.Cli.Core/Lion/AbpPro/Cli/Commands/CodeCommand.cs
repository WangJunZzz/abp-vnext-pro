using Lion.AbpPro.Cli.Dto;

namespace Lion.AbpPro.Cli.Commands;

public class CodeCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "code";
    private const string Dotnet = "dotnet";
    private const string Vue3 = "vue3";
    private readonly ILogger<CodeCommand> _logger;
    private readonly ICodeService _codeService;

    public CodeCommand(ICodeService codeService, ILogger<CodeCommand> logger)
    {
        _codeService = codeService;
        _logger = logger;
    }

    /// <summary>
    /// code -p 3a1821f9-9c71-4d5e-205e-008267cee6b9 -t 3a182572-1a21-ffd4-db9d-4d6065c4eac8 -s C:\Users\Administrator\.abp.pro\cli\code\output\ErpOA9.1.1-rc3\src
    /// </summary>
    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        var template = commandLineArgs.Options.GetOrNull(CommandOptions.Template.Short, CommandOptions.Template.Long);
        var project = commandLineArgs.Options.GetOrNull(CommandOptions.Project.Short, CommandOptions.Project.Long);
        var source = commandLineArgs.Options.GetOrNull(CommandOptions.Source.Short, CommandOptions.Source.Long);
        source = source.IsNullOrWhiteSpace() ? Directory.GetCurrentDirectory() : source;
        
        if (Guid.TryParse(template, out Guid templateId) && Guid.TryParse(project, out Guid projectId))
        {
            // 生成后端代码还是前端代码
            var type = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("请选择生成的代码?").AddChoices(new[]
            {
                Dotnet,
                Vue3
            }));

            if (type == Dotnet)
            {
                await GenerateBackCode(projectId, templateId, source);
            }
            else if (type == Vue3)
            {
                await GenerateFrontCode(projectId, templateId, source);
            }

            AnsiConsole.MarkupLine("[green]恭喜你,代码生成成功,请打开项目检查![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]输入的模板 ID 或项目 ID 不是有效的 GUID 格式，请检查后重新输入。[/]");
        }
    }

    private async Task<GetProjectAndEntityResponse> GetProjectAsync(string accessToken, Guid projectId)
    {
        var project = await _codeService.GetProjectAndEntityAsync(accessToken, projectId);
        if (project.Entities.Count == 0)
        {
            throw new UserFriendlyException("当前项目未定义实体");
        }

        return project;
    }

    /// <summary>
    /// 生成后端代码
    /// // https://spectreconsole.net/prompts/multiselection
    /// </summary>
    private async Task GenerateBackCode(Guid projectId, Guid templateId, string sourcePath)
    {
        var accessToken = await _codeService.GetAccessTokenAsync();

        // 获取项目信息
        var project = await GetProjectAsync(accessToken, projectId);

        //判断是否是标准abp的项目结构方式
        Utils.DirectoryHelper.IsAbpProjectStructure(sourcePath, project.Project.CompanyName, project.Project.ProjectName);

        // 可以选择需要生成的实体
        var entities = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("请选择需要生成的实体")
                .Required()
                .InstructionsText(
                    "[grey](按下空格键 [blue]<space>[/]切换是否选中, " +
                    "[white]上下建切换,[/] " +
                    "[green]按下回车键<enter>[/]确认)[/]")
                .AddChoices(project.Entities.Select(e => e.Code)));


        // 获取选中的实体信息
        var entityId = project.Entities.Where(e => entities.Contains(e.Code)).Select(p => p.Id).ToList();

        // 下载代码
        var path = await _codeService.DownloadAsync(accessToken, projectId, templateId, entityId);

        // 解压下载的代码
        var extractPath = ZipHelper.Extract(path);

        // 遍历选中实体生成对应层级的代码
        foreach (var id in entityId)
        {
            var item = project.Entities.First(e => e.Id == id);
            GenerateCode(extractPath, sourcePath, "Domain.Shared", item.CodePluralized, project.Project.CompanyName, project.Project.ProjectName);
            GenerateCode(extractPath, sourcePath, "Domain", item.CodePluralized, project.Project.CompanyName, project.Project.ProjectName);
            GenerateCode(extractPath, sourcePath, "Application.Contracts", item.CodePluralized, project.Project.CompanyName, project.Project.ProjectName);
            GenerateCode(extractPath, sourcePath, "Application", item.CodePluralized, project.Project.CompanyName, project.Project.ProjectName);
            GenerateCode(extractPath, sourcePath, "HttpApi", item.CodePluralized, project.Project.CompanyName, project.Project.ProjectName);
            AppendIDbContextCode(extractPath, sourcePath, "EntityFrameworkCore", item.CodePluralized, project.Project.CompanyName, project.Project.ProjectName);
            AppendDbContextCode(extractPath, sourcePath, "EntityFrameworkCore", item.CodePluralized, project.Project.CompanyName, project.Project.ProjectName);
            AppendDbContextModelCreatingExtensionsCode(extractPath, sourcePath, "EntityFrameworkCore", item.CodePluralized, project.Project.CompanyName, project.Project.ProjectName);
        }
    }

    /// <summary>
    /// 生成前端代码
    /// </summary>
    private async Task GenerateFrontCode(Guid projectId, Guid templateId, string sourcePath)
    {
        var accessToken = await _codeService.GetAccessTokenAsync();
        // 获取项目信息
        var project = await GetProjectAsync(accessToken, projectId);

        //判断是否是标准vben5的项目结构方式
        Utils.DirectoryHelper.IsVben5ProjectStructure(sourcePath);

        // 可以选择需要生成的实体
        var entities = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("请选择需要生成的实体")
                .Required()
                .InstructionsText(
                    "[grey](按下空格键 [blue]<space>[/]切换是否选中, " +
                    "[white]上下建切换,[/] " +
                    "[green]按下回车键<enter>[/]确认)[/]")
                .AddChoices(project.Entities.Select(e => e.Code)));


        // 获取选中的实体信息
        var entityId = project.Entities.Where(e => entities.Contains(e.Code)).Select(p => p.Id).ToList();

        // 下载代码
        var path = await _codeService.DownloadAsync(accessToken, projectId, templateId, entityId);

        // 解压下载的代码
        var extractPath = ZipHelper.Extract(path);

        // 遍历选中实体生成对应层级的代码
        foreach (var id in entityId)
        {
            var item = project.Entities.First(e => e.Id == id);
            var sourceCodePath = Path.Combine(extractPath, "Vben5", "routes", item.CodePluralized);
            var targetCodePath = Path.Combine(sourcePath, "router", "routes", "modules");
            Utils.DirectoryHelper.CopyFolder(sourceCodePath, targetCodePath);

            var sourceViewCodePath = Path.Combine(extractPath, "Vben5", "views", item.CodePluralized);
            var targetViewCodePath = Path.Combine(sourcePath, "views", item.CodePluralized);
            Utils.DirectoryHelper.CopyFolder(sourceViewCodePath, targetViewCodePath);
        }
    }

    private void GenerateCode(string templateSourceCodePath, string sourcePath, string type, string entityCodePluralized, string companyName, string projectName)
    {
        var sourceCodePath = Path.Combine(templateSourceCodePath, "AspNetCore", "src", type, entityCodePluralized);
        var targetCodePath = Path.Combine(sourcePath, $"{companyName}.{projectName}.{type}", entityCodePluralized);
        Utils.DirectoryHelper.CopyFolder(sourceCodePath, targetCodePath);
    }

    private void AppendIDbContextCode(string templateSourceCodePath, string sourcePath, string type, string entityCodePluralized, string companyName, string projectName)
    {
        var basePath = Path.Combine(templateSourceCodePath, "AspNetCore", "src", type);
        // 给IDbContext追加dbset
        var sourceCodePath = Path.Combine(basePath, $"I{projectName}DbContext.cs");
        var code = File.ReadAllText(sourceCodePath);

        var targetCodePath = Path.Combine(sourcePath, $"{companyName}.{projectName}.{type}", type, $"I{projectName}DbContext.cs");
        // 判断代码是否已经生成
        if (CodeHelper.IsExistCode(targetCodePath, code))
        {
            return;
        }

        Utils.CodeHelper.AddCodeToInterface(targetCodePath, $"I{projectName}DbContext", code);
        CodeHelper.AddUsing(targetCodePath, $"using {companyName}.{projectName}.{entityCodePluralized};");
    }

    private void AppendDbContextCode(string templateSourceCodePath, string sourcePath, string type, string entityCodePluralized, string companyName, string projectName)
    {
        var basePath = Path.Combine(templateSourceCodePath, "AspNetCore", "src", type);
        // 给DbContext追加dbset
        var dbContextCodePath = Path.Combine(basePath, $"{projectName}DbContext.cs");
        var dbContextCode = File.ReadAllText(dbContextCodePath);

        var targetDbContextCodePath = Path.Combine(sourcePath, $"{companyName}.{projectName}.{type}", type, $"{projectName}DbContext.cs");
        // 判断代码是否已经生成
        if (CodeHelper.IsExistCode(targetDbContextCodePath, dbContextCode))
        {
            return;
        }

        Utils.CodeHelper.AddCodeToClass(targetDbContextCodePath, $"{projectName}DbContext", dbContextCode);
        CodeHelper.AddUsing(targetDbContextCodePath, $"using {companyName}.{projectName}.{entityCodePluralized};");
    }

    private void AppendDbContextModelCreatingExtensionsCode(string templateSourceCodePath, string sourcePath, string type, string entityCodePluralized, string companyName, string projectName)
    {
        var basePath = Path.Combine(templateSourceCodePath, "AspNetCore", "src", type);
        // 给ContextModelCreatingExtensions追加ef 配置
        var dbContextModelCreatingExtensionsPath = Path.Combine(basePath, $"{projectName}DbContextModelCreatingExtensions.cs");
        var dbContextModelCreatingExtensionsCode = File.ReadAllText(dbContextModelCreatingExtensionsPath);
        var targetDbContextModelCreatingExtensionsCodePath = Path.Combine(sourcePath, $"{companyName}.{projectName}.{type}", type, $"{projectName}DbContextModelCreatingExtensions.cs");
        // 判断代码是否已经生成
        if (CodeHelper.IsExistCode(targetDbContextModelCreatingExtensionsCodePath, dbContextModelCreatingExtensionsCode))
        {
            return;
        }

        Utils.CodeHelper.AddCodeToMethod(targetDbContextModelCreatingExtensionsCodePath, $"Configure{projectName}", dbContextModelCreatingExtensionsCode);
        CodeHelper.AddUsing(targetDbContextModelCreatingExtensionsCodePath, $"using {companyName}.{projectName}.{entityCodePluralized};");
    }

    public void GetUsageInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  lion.abp create");
        sb.AppendLine("lion.abp create -t 模板名称(source | nuget) -c 公司名称 -p 项目名称");
        AnsiConsole.MarkupLine($"[green]{sb.ToString()}[/]");
    }

    public string GetShortDescription()
    {
        return "生成代码";
    }
}