namespace Lion.AbpPro.ElasticSearch;

public interface IElasticsearchProvider
{
    IElasticClient GetClient();
}