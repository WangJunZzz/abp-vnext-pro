using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Zzz.EntityFrameworkCore
{
    [DependsOn(
        typeof(ZzzEntityFrameworkCoreModule)
        )]
    public class ZzzEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ZzzMigrationsDbContext>();
        }
    }
}
