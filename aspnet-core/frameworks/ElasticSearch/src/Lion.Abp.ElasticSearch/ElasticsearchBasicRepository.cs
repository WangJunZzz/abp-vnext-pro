using Nest;
using Volo.Abp.Domain.Services;

namespace Lion.Abp.ElasticSearch
{
    public abstract class ElasticsearchBasicRepository : DomainService
    {
        private readonly IElasticsearchProvider _elasticsearchProvider;

        // ReSharper disable once PublicConstructorInAbstractClass
        public ElasticsearchBasicRepository(IElasticsearchProvider elasticsearchProvider)
        {
            _elasticsearchProvider = elasticsearchProvider;
        }

        protected IElasticClient Client => _elasticsearchProvider.GetElasticClient();
    }
}