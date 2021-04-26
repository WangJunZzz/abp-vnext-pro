using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace CompanyNameProjectName.EntityFrameworkCore
{
    [DependsOn(
        typeof(CompanyNameProjectNameEntityFrameworkCoreModule)
        )]
    public class CompanyNameProjectNameEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CompanyNameProjectNameMigrationsDbContext>();
        }
    }
}
