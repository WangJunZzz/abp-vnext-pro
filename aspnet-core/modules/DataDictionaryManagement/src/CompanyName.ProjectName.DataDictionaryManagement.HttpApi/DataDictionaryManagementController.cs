using CompanyName.ProjectName.DataDictionaryManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CompanyName.ProjectName.DataDictionaryManagement
{
    public abstract class DataDictionaryManagementController : AbpController
    {
        protected DataDictionaryManagementController()
        {
            LocalizationResource = typeof(DataDictionaryManagementResource);
        }
    }
}
