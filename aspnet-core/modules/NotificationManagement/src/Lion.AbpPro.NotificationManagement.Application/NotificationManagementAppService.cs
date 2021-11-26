using Lion.AbpPro.NotificationManagement.Localization;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.NotificationManagement
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
