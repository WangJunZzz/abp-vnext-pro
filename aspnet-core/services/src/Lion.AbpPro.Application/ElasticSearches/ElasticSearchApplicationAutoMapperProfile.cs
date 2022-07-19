namespace Lion.AbpPro.ElasticSearches
{
    public class ElasticSearchApplicationAutoMapperProfile : Profile
    {
        public ElasticSearchApplicationAutoMapperProfile()
        {
            CreateMap<PagingElasticSearchLogDto, PagingElasticSearchLogOutput>();
        }
    }
}