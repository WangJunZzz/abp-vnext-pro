using CompanyName.ProjectName.Extensions.Volo.Abp;
using Volo.Abp.Domain.Services;

namespace CompanyName.ProjectName.NotificationManagement
{
    public abstract class NotificationManagementDomainService : BaseDomainService
    {
        protected NotificationManagementDomainService()
        {
            ObjectMapperContext = typeof(NotificationManagementDomainModule);
        }
    }
}