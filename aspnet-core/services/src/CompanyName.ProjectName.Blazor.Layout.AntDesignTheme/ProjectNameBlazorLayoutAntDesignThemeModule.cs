using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.Blazor.Layout.AntDesignTheme
{
    [DependsOn(typeof(AbpAspNetCoreComponentsWebModule))]
    public class ProjectNameBlazorLayoutAntDesignThemeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(
                    typeof(ProjectNameBlazorLayoutAntDesignThemeModule).Assembly);
            });
        }
    }
}