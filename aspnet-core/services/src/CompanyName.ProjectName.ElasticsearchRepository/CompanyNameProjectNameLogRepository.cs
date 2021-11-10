using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.ElasticsearchRepository.Dto;
using Lion.Abp.Extension;
using Microsoft.Extensions.Configuration;
using Nest;

namespace CompanyName.ProjectName.ElasticsearchRepository
{
    public class CompanyNameProjectNameLogRepository : ElasticsearchBasicRepository,
        ICompanyNameProjectNameLogRepository
    {
        private readonly IConfiguration _configuration;

        public CompanyNameProjectNameLogRepository(
            IElasticsearchProvider elasticsearchProvider,
            IConfiguration configuration) : base(elasticsearchProvider)
        {
            _configuration = configuration;
        }

        public async Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(
            PagingElasticSearchLogInput input)
        {
            var IndexName =
                _configuration.GetValue<string>("ElasticSearch:SearchIndexFormat");
            // 默认查询当天
            input.StartCreationTime = input.StartCreationTime?.AddMilliseconds(-1) ??
                                      DateTime.Now.Date.AddMilliseconds(-1);
            input.EndCreationTime =
                input.EndCreationTime?.AddDays(1).AddMilliseconds(-1) ??
                DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
            var mustFilters =
                new List<Func<QueryContainerDescriptor<PagingElasticSearchLogOutput>,
                    QueryContainer>>
                {
                    t => t.DateRange(f =>
                        f.Field(fd => fd.CreationTime).TimeZone("Asia/Shanghai")
                            .GreaterThanOrEquals(input.StartCreationTime.Value)),
                    t => t.DateRange(
                        f => f.Field(fd => fd.CreationTime).TimeZone("Asia/Shanghai")
                            .LessThanOrEquals(input.EndCreationTime.Value))
                };

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                mustFilters.Add(t =>
                    t.MatchPhrase(f => f.Field(fd => fd.Message).Query(input.Filter.Trim())));
            }

            var result = await Client.SearchAsync<PagingElasticSearchLogOutput>(e => e
                .Index(IndexName)
                .From(input.SkipCount)
                .Size(input.PageSize)
                .Sort(s => s.Descending(sd => sd.CreationTime))
                .Query(q => q.Bool(qb => qb.Filter(mustFilters))));

            if (result.HitsMetadata != null)
            {
                return new CustomePagedResultDto<PagingElasticSearchLogOutput>(
                    result.HitsMetadata.Total.Value, result.Documents.ToList());
            }

            return null;
        }
    }
}