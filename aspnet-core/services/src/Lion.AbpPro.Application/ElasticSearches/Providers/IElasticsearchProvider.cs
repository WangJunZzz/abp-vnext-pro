namespace Lion.AbpPro.ElasticSearches.Providers
{
    public interface IElasticsearchProvider : ISingletonDependency
    {
        IElasticClient GetElasticClient();
    }
}