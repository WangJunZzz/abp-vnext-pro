using CompanyName.ProjectName.Blazor.WebAssembly;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.Web.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AbpAutofacWebAssemblyModule),
        typeof(ProjectNameBlazorWebAssemblyModule))]
    public class ProjectNameWebBlazorWebAssemblyModule : AbpModule
    {
    }
}
