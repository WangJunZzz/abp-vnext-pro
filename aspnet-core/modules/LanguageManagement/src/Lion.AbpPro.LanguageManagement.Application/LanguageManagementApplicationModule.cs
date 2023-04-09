namespace Lion.AbpPro.LanguageManagement
{
    [DependsOn(
        typeof(LanguageManagementDomainModule),
        typeof(LanguageManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class LanguageManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<LanguageManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<LanguageManagementApplicationModule>(validate: true);
            });
        }
    }
}
