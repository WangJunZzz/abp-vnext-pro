using System;
using System.Collections.Generic;
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
        private readonly string IndexName = "{0}.{1}*";
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;

        public CompanyNameProjectNameLogRepository(
            IElasticsearchProvider elasticsearchProvider,
            IConfiguration configuration,
            IHostEnvironment hostEnvironment) :
            base(elasticsearchProvider)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            IndexName = string.Format(IndexName, configuration["LogToElasticSearch:ElasticSearch:DashboardIndex"],
                _hostEnvironment.EnvironmentName.ToLower());
        }

        public async Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input)
        {
            // 默认查询当天
            input.StartCreationTime = input.StartCreationTime?.AddMilliseconds(-1) ?? DateTime.Now.Date.AddMilliseconds(-1);
            input.EndCreationTime =
                input.EndCreationTime?.AddDays(1).AddMilliseconds(-1) ?? DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
            var mustFilters = new List<Func<QueryContainerDescriptor<PagingElasticSearchLogOutput>, QueryContainer>>
            {
                t => t.DateRange(f =>
                    f.Field(fd => fd.CreationTime).TimeZone("Asia/Shanghai").GreaterThanOrEquals(input.StartCreationTime.Value)),
                t => t.DateRange(
                    f => f.Field(fd => fd.CreationTime).TimeZone("Asia/Shanghai").LessThanOrEquals(input.EndCreationTime.Value))
            };

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                mustFilters.Add(t => t.MatchPhrase(f => f.Field(fd => fd.Message).Query(input.Filter.Trim())));
            }

            var result = await Client.SearchAsync<PagingElasticSearchLogOutput>(e => e.Index(IndexName)
                .From(input.SkipCount)
                .Size(input.PageSize)
                .Sort(s => s.Descending(sd => sd.CreationTime))
                .Query(q => q.Bool(qb => qb.Filter(mustFilters))));

            return new CustomePagedResultDto<PagingElasticSearchLogOutput>(result.HitsMetadata.Total.Value, result.Documents.ToList());
        }
    }
}