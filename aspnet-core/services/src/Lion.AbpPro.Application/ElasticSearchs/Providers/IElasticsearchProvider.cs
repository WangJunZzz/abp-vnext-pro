using Nest;
using Volo.Abp.DependencyInjection;

namespace Lion.AbpPro.ElasticSearchs.Providers
{
    public interface IElasticsearchProvider : ISingletonDependency
    {
        IElasticClient GetElasticClient();
    }
}