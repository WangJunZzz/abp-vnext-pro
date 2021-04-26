using CompanyNameProjectName.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace CompanyNameProjectName.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(CompanyNameProjectNameEntityFrameworkCoreDbMigrationsModule),
        typeof(CompanyNameProjectNameApplicationContractsModule)
        )]
    public class CompanyNameProjectNameDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
