using Localization.Resources.AbpUi;
using Lion.AbpPro.NotificationManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Lion.AbpPro.NotificationManagement
{
    [DependsOn(
        typeof(NotificationManagementApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class NotificationManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(NotificationManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<NotificationManagementResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
