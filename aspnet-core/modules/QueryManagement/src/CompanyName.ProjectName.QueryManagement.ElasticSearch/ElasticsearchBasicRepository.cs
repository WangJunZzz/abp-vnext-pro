using Nest;
using Volo.Abp.Domain.Services;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearch
{
    public abstract class ElasticsearchBasicRepository : DomainService
    {
        private readonly IElasticsearchProvider _elasticsearchProvider;

        public ElasticsearchBasicRepository(IElasticsearchProvider elasticsearchProvider)
        {
            _elasticsearchProvider = elasticsearchProvider;
        }

        protected IElasticClient Client => _elasticsearchProvider.GetElasticClient();
    }
}