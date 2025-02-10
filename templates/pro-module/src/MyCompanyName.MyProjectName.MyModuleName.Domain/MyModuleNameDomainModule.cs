namespace MyCompanyName.MyProjectName.MyModuleName
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(MyModuleNameDomainSharedModule),
        typeof(AbpCachingModule),
        typeof(AbpAutoMapperModule)
    )]
    public class MyModuleNameDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 配置automapper
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MyModuleNameDomainModule>(validate: false);
            });
        }
    }
}
