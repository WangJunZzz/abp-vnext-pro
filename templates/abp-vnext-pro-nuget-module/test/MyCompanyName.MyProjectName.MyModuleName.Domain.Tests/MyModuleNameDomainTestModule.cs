namespace MyCompanyName.MyProjectName.MyModuleName
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(MyModuleNameEntityFrameworkCoreTestModule)
        )]
    public class MyModuleNameDomainTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MyModuleNameDomainModule>(validate: true);
            });
        }
    }
}
