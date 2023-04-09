using Lion.AbpPro.LanguageManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lion.AbpPro.LanguageManagement
{
    public abstract class LanguageManagementController : AbpController
    {
        protected LanguageManagementController()
        {
            LocalizationResource = typeof(LanguageManagementResource);
        }
    }
}
