using System;
using CompanyName.ProjectName.Blazor.Layout.AntDesignTheme;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.Blazor.WebAssembly
{
    [DependsOn(
        typeof(ProjectNameBlazorLayoutAntDesignThemeModule),
        typeof(ProjectNameHttpApiClientModule)
       )]
    public class ProjectNameBlazorWebAssemblyModule : AbpModule
    {
    }
}