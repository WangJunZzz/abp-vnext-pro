using CompanyName.ProjectName.QueryManagement.ElasticSearch;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace CompanyName.ProjectName.QueryManagement
{
    [DependsOn(
        typeof(QueryManagementDomainModule),
        typeof(QueryManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(QueryManagementElasticsearchModule)
        )]
    public class QueryManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<QueryManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<QueryManagementApplicationModule>(validate: true);
            });
        }
    }
}
