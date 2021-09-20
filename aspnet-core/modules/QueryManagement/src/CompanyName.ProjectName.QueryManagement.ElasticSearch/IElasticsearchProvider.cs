using System;
using Nest;
using Volo.Abp.DependencyInjection;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearch
{
    public interface IElasticsearchProvider : ISingletonDependency
    {
        IElasticClient GetElasticClient();
    }
}