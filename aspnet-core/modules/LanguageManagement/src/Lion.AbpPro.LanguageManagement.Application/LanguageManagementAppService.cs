namespace Lion.AbpPro.LanguageManagement
{
    public abstract class LanguageManagementAppService : ApplicationService
    {
        protected LanguageManagementAppService()
        {
            LocalizationResource = typeof(LanguageManagementResource);
            ObjectMapperContext = typeof(LanguageManagementApplicationModule);
        }
    }
}
