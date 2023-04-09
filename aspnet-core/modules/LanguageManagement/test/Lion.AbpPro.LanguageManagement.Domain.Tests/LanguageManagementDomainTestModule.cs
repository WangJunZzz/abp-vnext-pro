namespace Lion.AbpPro.LanguageManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(LanguageManagementEntityFrameworkCoreTestModule)
        )]
    public class LanguageManagementDomainTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<LanguageManagementDomainModule>(validate: true);
            });
        }
    }
}
