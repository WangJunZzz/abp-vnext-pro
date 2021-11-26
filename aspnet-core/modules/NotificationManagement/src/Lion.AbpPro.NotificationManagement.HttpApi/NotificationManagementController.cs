using Lion.AbpPro.NotificationManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lion.AbpPro.NotificationManagement
{
    public abstract class NotificationManagementController : AbpController
    {
        protected NotificationManagementController()
        {
            LocalizationResource = typeof(NotificationManagementResource);
        }
    }
}
