using Lion.AbpPro.LanguageManagement.Languages;
using Lion.AbpPro.LanguageManagement.LanguageTexts;

namespace Lion.AbpPro.LanguageManagement
{
    public class LanguageManagementApplicationAutoMapperProfile : Profile
    {
        public LanguageManagementApplicationAutoMapperProfile()
        {
            CreateMap<LanguageDto, PageLanguageOutput>();
            CreateMap<Language, PageLanguageOutput>();
            CreateMap<LanguageTextDto, PageLanguageTextOutput>();
        }
    }
}