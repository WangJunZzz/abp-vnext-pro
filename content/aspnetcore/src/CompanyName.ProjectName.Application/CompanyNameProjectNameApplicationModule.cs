using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace CompanyNameProjectName
{
    [DependsOn(
        typeof(CompanyNameProjectNameDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(CompanyNameProjectNameApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(EasyAbp.Abp.SettingUi.SettingUiApplicationModule)
        )]
    public class CompanyNameProjectNameApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
           
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CompanyNameProjectNameApplicationModule>();
            });
        }
    }
}
