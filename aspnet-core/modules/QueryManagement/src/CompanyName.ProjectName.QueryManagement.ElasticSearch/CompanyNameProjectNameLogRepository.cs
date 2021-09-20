using System;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.Extensions.Customs.Dtos;
using CompanyName.ProjectName.QueryManagement.ElasticSearchs;
using CompanyName.ProjectName.QueryManagement.ElasticSearchs.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Nest;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearch
{
    public class CompanyNameProjectNameLogRepository : ElasticsearchBasicRepository, ICompanyNameProjectNameLogRepository
    {
        private readonly string IndexName = "{0}*";
        private readonly IConfiguration _configuration;

        public CompanyNameProjectNameLogRepository(
            IElasticsearchProvider elasticsearchProvider,
            IConfiguration configuration) :
            base(elasticsearchProvider)
        {
            _configuration = configuration;
            IndexName = string.Format(IndexName, configuration["LogToElasticSearch:ElasticSearch:IndexFormat"]);
        }

        public async Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input)
        {
            // 默认查询15分钟
            input.StartCreationTime ??= DateTime.Now.AddMinutes(1);

            input.EndCreationTime ??= DateTime.Now.AddMinutes(-15);

            if (string.IsNullOrWhiteSpace(input.Filter))
            {
                var result = await Client.SearchAsync<PagingElasticSearchLogOutput>(
                    e => e.Index(IndexName)
                        .Query(q => q.DateRange(c =>
                            c.Name("@timestamp").GreaterThan(input.StartCreationTime).LessThan(input.EndCreationTime)))
                        .Sort(s => s.Descending(d => d.CreationTime)).From(input.SkipCount).Size(input.PageSize));
                return new CustomePagedResultDto<PagingElasticSearchLogOutput>(result.HitsMetadata.Total.Value, result.Documents.ToList());
            }
            else
            {
                var result = await Client.SearchAsync<PagingElasticSearchLogOutput>(
                    e => e.Index(IndexName)
                        .Query(q => q.DateRange(c =>
                            c.Name("@timestamp").GreaterThan(input.StartCreationTime).LessThan(input.EndCreationTime))).Query(q =>
                            q.MatchPhrase(m => m.Field(f => f.Message)
                                .Query(input.Filter.Trim()))).Sort(s => s.Descending(d => d.CreationTime)).From(input.SkipCount)
                        .Size(input.PageSize));
                return new CustomePagedResultDto<PagingElasticSearchLogOutput>(result.HitsMetadata.Total.Value, result.Documents.ToList());
            }
        }
    }
}