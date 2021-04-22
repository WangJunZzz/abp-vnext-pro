using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Zzz
{
    [DependsOn(
        typeof(ZzzDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(ZzzApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(EasyAbp.Abp.SettingUi.SettingUiApplicationModule)
        )]
    public class ZzzApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ZzzApplicationModule>();
            });
        }
    }
}
