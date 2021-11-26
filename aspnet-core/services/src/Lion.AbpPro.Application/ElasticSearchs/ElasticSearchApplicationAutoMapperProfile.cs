using AutoMapper;
using Lion.AbpPro.ElasticsearchRepository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion.AbpPro.ElasticSearchs
{
    public class ElasticSearchApplicationAutoMapperProfile : Profile
    {
        public ElasticSearchApplicationAutoMapperProfile()
        {
            CreateMap<PagingElasticSearchLogDto, PagingElasticSearchLogOutput>();
        }
    }
}