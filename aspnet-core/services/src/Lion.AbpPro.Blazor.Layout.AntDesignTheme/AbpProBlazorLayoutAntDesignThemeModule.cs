using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.Blazor.Layout.AntDesignTheme
{
    [DependsOn(
        typeof(AbpProApplicationContractsModule)
    )]
    public class AbpProBlazorLayoutAntDesignThemeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAntDesign();
        }
    }
}
