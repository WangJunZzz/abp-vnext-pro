using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.QueryManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(QueryManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class QueryManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<QueryManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}