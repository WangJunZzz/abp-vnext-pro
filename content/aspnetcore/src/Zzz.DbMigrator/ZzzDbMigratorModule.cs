using Zzz.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Zzz.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ZzzEntityFrameworkCoreDbMigrationsModule),
        typeof(ZzzApplicationContractsModule)
        )]
    public class ZzzDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
