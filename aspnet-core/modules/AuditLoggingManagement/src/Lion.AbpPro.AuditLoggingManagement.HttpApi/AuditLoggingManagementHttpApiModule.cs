using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.AuditLoggingManagement
{
    [DependsOn(
    typeof(AuditLoggingManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
    public class AuditLoggingManagementHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                //options.Resources
                //    .Get<AuditlogResource>()
                //    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
