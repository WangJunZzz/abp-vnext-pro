using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.DataDictionaryManagement;
using Lion.AbpPro.NotificationManagement;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainSharedModule),
        typeof(BasicManagementDomainModule),
        typeof(NotificationManagementDomainModule),
        typeof(DataDictionaryManagementDomainModule)
    )]
    public class MyProjectNameDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<MyProjectNameDomainModule>(); });
        }
    }
}