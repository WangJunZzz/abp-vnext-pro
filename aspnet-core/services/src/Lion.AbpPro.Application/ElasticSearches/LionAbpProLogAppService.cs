namespace Lion.AbpPro.ElasticSearches
{
    [Authorize]
    public class LionAbpProLogAppService : ElasticsearchBasicService,ILionAbpProLogAppService
    {
        private readonly IConfiguration _configuration;
        // 时区
        private const string TimeZone = "Asia/Shanghai";
        public LionAbpProLogAppService(
            IElasticsearchProvider elasticsearchProvider,
            IConfiguration configuration) : base(elasticsearchProvider)
        {
            _configuration = configuration;
        }
        [Authorize(Policy = AbpProPermissions.SystemManagement.ES)]
        public async Task<CustomePagedResultDto<PagingElasticSearchLogOutput>> PaingAsync(PagingElasticSearchLogInput input)
        {
            var IndexName = _configuration.GetValue<string>("ElasticSearch:SearchIndexFormat");
            // 默认查询当天
            input.StartCreationTime = input.StartCreationTime?.AddMilliseconds(-1) ??DateTime.Now.Date.AddMilliseconds(-1);
            input.EndCreationTime =input.EndCreationTime?.AddDays(1).AddMilliseconds(-1) ??DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
            var mustFilters = new List<Func<QueryContainerDescriptor<PagingElasticSearchLogDto>, QueryContainer>>();
            if (input.StartCreationTime.HasValue)
            {
                input.StartCreationTime = input.StartCreationTime.ToCurrentDateMaxDateTime();
                mustFilters.Add(e => e.DateRange(f => f.Field(fd => fd.CreationTime).TimeZone(TimeZone).GreaterThanOrEquals(input.StartCreationTime)));
            }

            if (input.EndCreationTime.HasValue)
            {
                input.EndCreationTime = input.EndCreationTime.ToNextSecondDateTime();
                mustFilters.Add(e => e.DateRange(f => f.Field(fd => fd.CreationTime).TimeZone(TimeZone).LessThanOrEquals(input.EndCreationTime)));
            }

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                mustFilters.Add
                (
                    t =>t.MatchPhrase(f => f.Field(fd => fd.Message).Query(input.Filter.Trim()))
                );
            }

            var result = await Client.SearchAsync<PagingElasticSearchLogDto>
            (
                e => e
                    .Index(IndexName)
                    .From(input.SkipCount)
                    .Size(input.PageSize)
                    .Sort(s => s.Descending(sd => sd.CreationTime))
                    .Query(q => q.Bool(qb => qb.Filter(mustFilters)))
            );

            if (result.HitsMetadata != null)
            {
                return new CustomePagedResultDto<PagingElasticSearchLogOutput>
                (
                    result.HitsMetadata.Total.Value,
                    ObjectMapper
                        .Map<List<PagingElasticSearchLogDto>, List<PagingElasticSearchLogOutput>>
                        (
                            result.Documents.ToList()
                        )
                );
            }

            return null;
        }
    }
}