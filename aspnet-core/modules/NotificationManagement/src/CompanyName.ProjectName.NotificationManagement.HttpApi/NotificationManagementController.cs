using CompanyName.ProjectName.NotificationManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CompanyName.ProjectName.NotificationManagement
{
    public abstract class NotificationManagementController : AbpController
    {
        protected NotificationManagementController()
        {
            LocalizationResource = typeof(NotificationManagementResource);
        }
    }
}
