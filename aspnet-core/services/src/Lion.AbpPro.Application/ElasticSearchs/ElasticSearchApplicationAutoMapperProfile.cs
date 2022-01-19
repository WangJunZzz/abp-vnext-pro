using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lion.AbpPro.ElasticSearchs.Dto;

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