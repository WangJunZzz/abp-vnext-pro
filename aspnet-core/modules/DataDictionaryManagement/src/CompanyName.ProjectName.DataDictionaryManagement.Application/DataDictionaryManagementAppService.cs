using CompanyName.ProjectName.DataDictionaryManagement.Localization;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.DataDictionaryManagement
{
    public abstract class DataDictionaryManagementAppService : ApplicationService
    {
        protected DataDictionaryManagementAppService()
        {
            LocalizationResource = typeof(DataDictionaryManagementResource);
            ObjectMapperContext = typeof(DataDictionaryManagementApplicationModule);
        }
    }
}
