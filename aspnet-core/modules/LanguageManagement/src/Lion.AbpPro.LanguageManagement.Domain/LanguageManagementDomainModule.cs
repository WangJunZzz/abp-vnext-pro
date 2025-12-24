using Volo.Abp.SettingManagement;

namespace Lion.AbpPro.LanguageManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(LanguageManagementDomainSharedModule),
        typeof(AbpCachingModule),
        typeof(AbpSettingManagementDomainModule)
    )]
    public class LanguageManagementDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.AddDynamicResource();
            });
        }
    }
}