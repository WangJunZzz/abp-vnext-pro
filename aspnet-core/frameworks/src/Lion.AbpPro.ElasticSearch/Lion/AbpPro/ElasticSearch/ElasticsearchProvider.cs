namespace Lion.AbpPro.ElasticSearch;

public class ElasticsearchProvider : IElasticsearchProvider, ISingletonDependency
{
    private readonly AbpProElasticSearchOptions _options;

    public ElasticsearchProvider(IOptions<AbpProElasticSearchOptions> options)
    {
        _options = options.Value;
    } 

    public virtual IElasticClient GetClient()
    {
        var connectionPool = new SingleNodeConnectionPool(new Uri(_options.Host));
        var settings = new ConnectionSettings(connectionPool);
        settings.BasicAuthentication(_options.UserName, _options.Password);
        return new ElasticClient(settings);
    }
}