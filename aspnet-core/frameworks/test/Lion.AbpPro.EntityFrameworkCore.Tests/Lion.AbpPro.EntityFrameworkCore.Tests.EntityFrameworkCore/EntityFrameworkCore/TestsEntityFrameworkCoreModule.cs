using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.EntityFrameworkCore;

[DependsOn(
    typeof(TestsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class TestsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TestsDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
