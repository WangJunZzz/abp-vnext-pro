using Nest;
using Volo.Abp.Domain.Services;

namespace CompanyName.ProjectName.ElasticsearchRepository
{
    public abstract class ElasticsearchBasicService : ProjectNameAppService
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