using AutoMapper;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Aggregates;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dto;

namespace Lion.AbpPro.DataDictionaryManagement
{
    public class DataDictionaryDomainAutoMapperProfile : Profile
    {
        public DataDictionaryDomainAutoMapperProfile()
        {
            CreateMap<DataDictionary, DataDictionaryDto>();
            CreateMap<DataDictionaryDetail, DataDictionaryDetailDto>();
        }
    }
}