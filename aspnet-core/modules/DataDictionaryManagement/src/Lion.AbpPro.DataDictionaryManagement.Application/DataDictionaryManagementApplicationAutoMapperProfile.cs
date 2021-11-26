using AutoMapper;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Aggregates;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos;

namespace Lion.AbpPro.DataDictionaryManagement
{
    public class DataDictionaryManagementApplicationAutoMapperProfile : Profile
    {
        public DataDictionaryManagementApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<DataDictionary, PagingDataDictionaryOutput>();
            CreateMap<DataDictionaryDetail, PagingDataDictionaryDetailOutput>();
        }
    }
}