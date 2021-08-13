using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.NotificationManagement.MongoDB
{
    [DependsOn(
        typeof(NotificationManagementDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class NotificationManagementMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<NotificationManagementMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
