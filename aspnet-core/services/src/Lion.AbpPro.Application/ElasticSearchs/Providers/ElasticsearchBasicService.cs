using Nest;

namespace Lion.AbpPro.ElasticSearchs.Providers
{
    public abstract class ElasticsearchBasicService : AbpProAppService
    {
        private readonly IElasticsearchProvider _elasticsearchProvider;

        // ReSharper disable once PublicConstructorInAbstractClass
        public ElasticsearchBasicService(IElasticsearchProvider elasticsearchProvider)
        {
            _elasticsearchProvider = elasticsearchProvider;
        }

        protected IElasticClient Client => _elasticsearchProvider.GetElasticClient();
    }
}