using CompanyName.ProjectName.NotificationManagement.Localization;
using Volo.Abp.Application.Services;

namespace CompanyName.ProjectName.NotificationManagement
{
    public abstract class NotificationManagementAppService : ApplicationService
    {
        protected NotificationManagementAppService()
        {
            LocalizationResource = typeof(NotificationManagementResource);
            ObjectMapperContext = typeof(NotificationManagementApplicationModule);
        }
    }
}
