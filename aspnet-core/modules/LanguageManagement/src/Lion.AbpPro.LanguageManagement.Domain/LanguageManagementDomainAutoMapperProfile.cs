using Lion.AbpPro.LanguageManagement.Languages;
using Lion.AbpPro.LanguageManagement.LanguageTexts;

namespace Lion.AbpPro.LanguageManagement
{
    public class LanguageManagementDomainAutoMapperProfile : Profile
    {
        public LanguageManagementDomainAutoMapperProfile()
        {
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageText, LanguageTextDto>();
            CreateMap<Language, LanguageInfo>();
        }
    }
}