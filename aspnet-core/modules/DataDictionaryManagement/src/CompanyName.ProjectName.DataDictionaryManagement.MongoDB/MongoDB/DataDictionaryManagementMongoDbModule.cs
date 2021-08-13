using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace CompanyName.ProjectName.DataDictionaryManagement.MongoDB
{
    [DependsOn(
        typeof(DataDictionaryManagementDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class DataDictionaryManagementMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<DataDictionaryManagementMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
