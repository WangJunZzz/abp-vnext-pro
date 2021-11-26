using Lion.AbpPro.DataDictionaryManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lion.AbpPro.DataDictionaryManagement
{
    public abstract class DataDictionaryManagementController : AbpController
    {
        protected DataDictionaryManagementController()
        {
            LocalizationResource = typeof(DataDictionaryManagementResource);
        }
    }
}
