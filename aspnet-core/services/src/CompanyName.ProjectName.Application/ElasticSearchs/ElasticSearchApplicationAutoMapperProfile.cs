using AutoMapper;
using CompanyName.ProjectName.ElasticsearchRepository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.ElasticSearchs
{
    public class ElasticSearchApplicationAutoMapperProfile : Profile
    {
        public ElasticSearchApplicationAutoMapperProfile()
        {
            CreateMap<PagingElasticSearchLogDto, PagingElasticSearchLogOutput>();
        }
    }
}