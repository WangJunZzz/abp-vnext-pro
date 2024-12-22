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

        Configure<Options.AbpProCliOptions>(options =>
        {
            options.Owner = "WangJunZzz";
            options.RepositoryId = "abp-vnext-pro";
            options.Token = "abp-vnext-proghp_47vqiabp-vnext-provNkHKJguOJkdHvnxUabp-vnext-protij7Qbdn1Qy3fUabp-vnext-pro";
            options.Templates = new List<AbpProTemplateOptions>()
            {
                new AbpProTemplateOptions("abp-vnext-pro", "pro", "源码版本", true)
                {
                    ExcludeFiles = "templates,docs,.github,LICENSE,Readme.md",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets",
                    OldCompanyName = "Lion",
                    OldProjectName = "AbpPro"
                },
                new AbpProTemplateOptions("abp-vnext-pro-nuget-all", "pro.all", "Nuget完整版本")
                {
                    //ExcludeFiles = "aspnet-core,vben28,abp-vnext-pro-nuget-module,abp-vnext-pro-nuget-simplify,docs,.github,LICENSE,Readme.md",
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

                new AbpProTemplateOptions("abp-vnext-pro-nuget-module", "pro.module", "模块")
                {
                    //ExcludeFiles = "aspnet-core,vben28,abp-vnext-pro-nuget-all,abp-vnext-pro-nuget-simplify,docs,.github,LICENSE,Readme.md",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets",
                    OldCompanyName = "MyCompanyName",
                    OldProjectName = "MyProjectName",
                    OldModuleName = "MyModuleName",
                },
                new AbpProTemplateOptions("abp-vnext-pro-business", "local", "local")
                {
                    //ExcludeFiles = "aspnet-core,vben28,abp-vnext-pro-nuget-module,abp-vnext-pro-nuget-simplify,docs,.github,LICENSE,Readme.md",
                    ReplaceSuffix = ".sln,.csproj,.cs,.cshtml,.json,.ci,.yml,.yaml,.nswag,.DotSettings,.env,Directory.Build.Lion.targets",
                    OldCompanyName = "Lion",
                    OldProjectName = "AbpPro"
                },
            };
        });

        context.Services.AddHttpClient();
    }
}