using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.Blazor.Layout.AntDesignTheme
{
    public class AbpProBlazorLayoutAntDesignThemeModule: AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(AbpProBlazorLayoutAntDesignThemeModule).Assembly);
            });
        }
    }
}
