namespace MyCompanyName.MyProjectName.MyModuleName
{
    [DependsOn(
        typeof(MyModuleNameDomainModule),
        typeof(MyModuleNameApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class MyModuleNameApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MyModuleNameApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MyModuleNameApplicationModule>(validate: true);
            });
        }
    }
}
