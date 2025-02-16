using Lion.AbpPro.FileManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProDomainModule),
        typeof(AbpProApplicationContractsModule),
        typeof(BasicManagementApplicationModule),
        typeof(DataDictionaryManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(LanguageManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(FileManagementApplicationModule),
        typeof(AbpProFreeSqlModule)
    )]
    public class AbpProApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<AbpProApplicationModule>(); });
        }
    }
}