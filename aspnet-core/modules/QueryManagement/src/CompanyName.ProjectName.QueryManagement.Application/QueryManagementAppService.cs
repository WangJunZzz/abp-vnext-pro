using CompanyName.ProjectName.QueryManagement.Localization;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.QueryManagement
{
    public abstract class QueryManagementAppService : ApplicationService
    {
        protected QueryManagementAppService()
        {
            LocalizationResource = typeof(QueryManagementResource);
            ObjectMapperContext = typeof(QueryManagementApplicationModule);
        }
    }
}
