namespace Lion.AbpPro.Cli;

[DependsOn(
    typeof(AbpDddDomainModule)
)]
public class AbpProCliCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpCliOptions>(options => { options.Commands[HelpCommand.Name] = typeof(HelpCommand); });
        Configure<AbpCliOptions>(options => { options.Commands[NewCommand.Name] = typeof(NewCommand); });
        Configure<AbpCliOptions>(options => { options.Commands[LoginCommand.Name] = typeof(LoginCommand); });
        Configure<AbpCliOptions>(options => { options.Commands[CreateCommand.Name] = typeof(CreateCommand); });

        Configure<Options.AbpProCliOptions>(options =>
        {
            options.Owner = "WangJunZzz";
            options.RepositoryId = "abp-vnext-pro";
            options.Token = "abp-vnext-proghp_47vqiabp-vnext-provNkHKJguOJkdHvnxUabp-vnext-protij7Qbdn1Qy3fUabp-vnext-pro";
            options.Templates = new List<AbpProTemplateOptions>()
            {
                new AbpProTemplateOptions("pro", "pro", "源码版本", true)
                {
                    ExcludeFiles = "templates,docs,.github,LICENSE,.idea,Lion.AbpPro.Cli.sln,Lion.AbpPro.Cli.sln.DotSettings.user",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets",
                    OldCompanyName = "Lion",
                    OldProjectName = "AbpPro"
                },
                new AbpProTemplateOptions("pro-nuget", "pro-nuget", "Nuget完整版本")
                {
                    ExcludeFiles = "aspnet-core,pro-module,docs,.github,LICENSE,.idea,Lion.AbpPro.Cli.sln,Lion.AbpPro.Cli.sln.DotSettings.user",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets",
                    OldCompanyName = "MyCompanyName",
                    OldProjectName = "MyProjectName"
                },
                // new AbpProTemplateOptions("abp-vnext-pro-nuget-simplify", "pro.simplify", "Nuget简单版本")
                // {
                //     //ExcludeFiles = "aspnet-core,vben28,abp-vnext-pro-nuget-module,abp-vnext-pro-nuget-all,docs,.github,LICENSE,Readme.md",
                //     ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets",
                //     OldCompanyName = "MyCompanyName",
                //     OldProjectName = "MyProjectName"
                // },

                new AbpProTemplateOptions("pro-module", "pro-module", "模块")
                {
                    ExcludeFiles = "aspnet-core,vben28,abp-nuget,docs,.github,LICENSE,.idea,Lion.AbpPro.Cli.sln,Lion.AbpPro.Cli.sln.DotSettings.user",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets",
                    OldCompanyName = "MyCompanyName",
                    OldProjectName = "MyProjectName",
                    OldModuleName = "MyModuleName",
                }
            };
        });

        Configure<Options.AbpProCliBusinessOptions>(options =>
        {
            options.Owner = "abp-vnext-pro";
            options.RepositoryId = "abp";
            options.Templates = new List<AbpProTemplateOptions>()
            {
                new AbpProTemplateOptions("pro", "pro", "商业版本源码版本")
                {
                    ExcludeFiles = ".github,LICENSE,Readme.md,.templates,Lion.AbpPro.Cli.sln",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets",
                    OldCompanyName = "Lion",
                    OldProjectName = "AbpPro",
                    OldModuleName = "",
                },
                
                new AbpProTemplateOptions("pro-nuget", "pro-nuget", "商业版本nuget版本")
                {
                    ExcludeFiles = ".github,LICENSE,Readme.md,aspnet-core,.idea,gateways,MyCompanyName.MyProjectName.Gateways.sln,MyCompanyName.MyProjectName.Gateways.sln.DotSettings.user",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets,.sln.DotSettings.user",
                    OldCompanyName = "MyCompanyName",
                    OldProjectName = "MyProjectName",
                    OldModuleName = "",
                },
                
                new AbpProTemplateOptions("pro-nuget-gateways", "pro-nuget", "商业版本nuget网关版本")
                {
                    ExcludeFiles = ".github,LICENSE,Readme.md,aspnet-core,.idea,MyCompanyName.MyProjectName.sln,MyCompanyName.MyProjectName.sln.DotSettings.user,Lion.AbpPro.Cli.sln",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets,.sln.DotSettings.user",
                    OldCompanyName = "MyCompanyName",
                    OldProjectName = "MyProjectName",
                    OldModuleName = "",
                },
            };
        });
        context.Services.AddHttpClient();
    }
}