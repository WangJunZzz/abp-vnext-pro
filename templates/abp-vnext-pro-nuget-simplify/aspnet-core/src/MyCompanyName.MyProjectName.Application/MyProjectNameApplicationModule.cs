namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainModule),
        typeof(MyProjectNameApplicationContractsModule),
        typeof(BasicManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(DataDictionaryManagementApplicationModule),
        typeof(LanguageManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(MyProjectNameFreeSqlModule)
        )]
    public class MyProjectNameApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MyProjectNameApplicationModule>();
            });
            
        }
    }
}
