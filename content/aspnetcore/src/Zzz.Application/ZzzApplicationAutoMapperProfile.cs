using AutoMapper;
using Volo.Abp.Identity;
using Zzz.Dic;
using Zzz.DTOs.Dic;
using Zzz.DTOs.Users;

namespace Zzz
{
    public class ZzzApplicationAutoMapperProfile : Profile
    {
        public ZzzApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<IdentityUser, LoginOutputDto>();
            CreateMap<DataDictionary, GetDataDictionaryDto>();
            CreateMap<DataDictionaryDetail, GetDataDictionaryDetailDto>();
        }
    }
}
