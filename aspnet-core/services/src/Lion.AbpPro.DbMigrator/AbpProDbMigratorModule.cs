using Lion.AbpPro.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

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
