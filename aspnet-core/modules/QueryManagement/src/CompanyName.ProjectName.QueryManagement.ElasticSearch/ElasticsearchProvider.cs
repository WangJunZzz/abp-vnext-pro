using System;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using Volo.Abp.DependencyInjection;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearch
{
    public class ElasticsearchProvider : IElasticsearchProvider, ISingletonDependency
    {
        private readonly IConfiguration _configuration;

        public ElasticsearchProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IElasticClient GetElasticClient()
        {
            var pool = new SingleNodeConnectionPool(new Uri(_configuration.GetValue<string>("LogToElasticSearch:ElasticSearch:Url")));
            var connectionSettings =
                new ConnectionSettings(pool);
            connectionSettings.EnableHttpCompression();
            connectionSettings.BasicAuthentication(_configuration.GetValue<string>("LogToElasticSearch:ElasticSearch:UserName"),
                _configuration.GetValue<string>("LogToElasticSearch:ElasticSearch:Password"));

            return new ElasticClient(connectionSettings);
        }
    }
}