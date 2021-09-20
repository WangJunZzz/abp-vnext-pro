using Volo.Abp.Autofac;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearch
{
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpDddDomainModule))]
    public class QueryManagementElasticsearchModule : AbpModule
    {
    }
}