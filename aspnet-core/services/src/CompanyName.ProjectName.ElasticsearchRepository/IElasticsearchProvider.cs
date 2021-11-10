using Nest;
using Volo.Abp.DependencyInjection;

namespace CompanyName.ProjectName.ElasticsearchRepository
{
    public interface IElasticsearchProvider : ISingletonDependency
    {
        IElasticClient GetElasticClient();
    }
}