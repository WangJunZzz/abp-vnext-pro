using Nest;
using Volo.Abp.Domain.Services;

namespace Lion.AbpPro.ElasticsearchRepository
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