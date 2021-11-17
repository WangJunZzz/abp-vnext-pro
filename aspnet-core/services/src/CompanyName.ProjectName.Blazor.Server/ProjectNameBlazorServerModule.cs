using System;
using CompanyName.ProjectName.Blazor.Layout.AntDesignTheme;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.Blazor.Server
{
    [DependsOn(typeof(ProjectNameBlazorLayoutAntDesignThemeModule))]
    public class ProjectNameBlazorServerModule : AbpModule
    {
    }
}