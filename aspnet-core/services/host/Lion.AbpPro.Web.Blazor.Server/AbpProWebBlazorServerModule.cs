using Lion.AbpPro.Blazor.Layout.AntDesignTheme;
using Lion.AbpPro.EntityFrameworkCore;
using Lion.AbpPro.Web.Blazor.Server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.Web.Blazor.Server
{
    [DependsOn(typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpProApplicationModule),
        typeof(AbpProHttpApiModule),
        typeof(AbpProEntityFrameworkCoreModule),
        typeof(AbpAccountWebModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpProBlazorLayoutAntDesignThemeModule)
        )]
    public class AbpProWebBlazorServerModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRazorPages();
            context.Services.AddServerSideBlazor();
            context.Services.AddSingleton<WeatherForecastService>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();
            // app.MapBlazorHub();
            // app.MapFallbackToPage("/_Host");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
