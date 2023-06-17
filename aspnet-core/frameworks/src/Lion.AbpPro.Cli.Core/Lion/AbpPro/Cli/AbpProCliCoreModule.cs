using Lion.AbpPro.Cli.Commands;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

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

        Configure<Options.LionAbpProOptions>(options =>
        {
            options.Owner = "WangJunZzz";
            options.RepositoryId = "abp-vnext-pro";
            options.Token = "abp-vnext-proghp_47vqiabp-vnext-provNkHKJguOJkdHvnxUabp-vnext-protij7Qbdn1Qy3fUabp-vnext-pro";
            options.Templates = new List<LionAbpProTemplateOptions>()
            {
                new LionAbpProTemplateOptions("abp-vnext-pro", "abp.vnext.pro", "源码版本", true)
                {
                    ExcludeFiles = "templates,docs,.github,LICENSE,Readme.md",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env",
                },
                new LionAbpProTemplateOptions("abp-vnext-pro-nuget-all", "abp.vnext.pro.nuget.all", "Nuget完整版本")
                {
                    ExcludeFiles = "aspnet-core,vben28,abp-vnext-pro-nuget-module,abp-vnext-pro-nuget-simplify,docs,.github,LICENSE,Readme.md",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env",
                },
                new LionAbpProTemplateOptions("abp-vnext-pro-nuget-simplify", "abp.vnext.pro.nuget.simplify", "Nuget简单版本")
                {
                    ExcludeFiles = "aspnet-core,vben28,abp-vnext-pro-nuget-module,abp-vnext-pro-nuget-all,docs,.github,LICENSE,Readme.md",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env",
                },

                new LionAbpProTemplateOptions("abp-vnext-pro-nuget-module", "abp.vnext.pro.nuget.module", "模块")
                {
                    ExcludeFiles = "aspnet-core,vben28,abp-vnext-pro-nuget-all,abp-vnext-pro-nuget-simplify,docs,.github,LICENSE,Readme.md",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env",
                },
            };
        });

        context.Services.AddHttpClient();
    }
}