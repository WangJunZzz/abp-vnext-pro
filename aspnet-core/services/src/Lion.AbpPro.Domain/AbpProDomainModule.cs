namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProDomainSharedModule),
        typeof(AbpEmailingModule),
        typeof(BasicManagementDomainModule),
        typeof(DataDictionaryManagementDomainModule),
        typeof(NotificationManagementDomainModule),
        typeof(LanguageManagementDomainModule)
    )]
    public class AbpProDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<AbpProDomainModule>(); });
        }
    }
}