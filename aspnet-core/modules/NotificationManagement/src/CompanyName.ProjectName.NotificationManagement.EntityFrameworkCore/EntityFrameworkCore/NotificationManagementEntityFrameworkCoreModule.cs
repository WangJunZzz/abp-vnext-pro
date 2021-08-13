using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.NotificationManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(NotificationManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class NotificationManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<NotificationManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}