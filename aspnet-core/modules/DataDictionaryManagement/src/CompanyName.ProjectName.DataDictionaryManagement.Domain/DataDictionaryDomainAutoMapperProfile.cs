using AutoMapper;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Aggregates;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Dto;

namespace CompanyName.ProjectName.DataDictionaryManagement
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