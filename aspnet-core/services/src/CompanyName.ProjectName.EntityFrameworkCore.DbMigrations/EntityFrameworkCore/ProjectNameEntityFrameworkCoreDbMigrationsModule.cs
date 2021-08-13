using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.EntityFrameworkCore
{
    [DependsOn(
        typeof(ProjectNameEntityFrameworkCoreModule)
        )]
    public class ProjectNameEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ProjectNameMigrationsDbContext>();
        }
    }
}
