using CompanyName.ProjectName.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ProjectNameEntityFrameworkCoreModule),
        typeof(ProjectNameApplicationContractsModule)
        )]
    public class ProjectNameDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
