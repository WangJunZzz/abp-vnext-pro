using Nest;
using Volo.Abp.DependencyInjection;

namespace Lion.Abp.ElasticSearch
{
    public interface IElasticsearchProvider : ISingletonDependency
    {
        IElasticClient GetElasticClient();
    }
}