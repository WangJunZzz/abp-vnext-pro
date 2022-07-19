namespace Lion.AbpPro.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpProEntityFrameworkCoreModule),
        typeof(AbpProApplicationContractsModule)
        )]
    public class AbpProDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
