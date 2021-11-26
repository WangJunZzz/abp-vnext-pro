using Nest;
using Volo.Abp.DependencyInjection;

namespace Lion.AbpPro.ElasticsearchRepository
{
    public interface IElasticsearchProvider : ISingletonDependency
    {
        IElasticClient GetElasticClient();
    }
}