using CompanyName.ProjectName.QueryManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CompanyName.ProjectName.QueryManagement
{
    public abstract class QueryManagementController : AbpController
    {
        protected QueryManagementController()
        {
            LocalizationResource = typeof(QueryManagementResource);
        }
    }
}
