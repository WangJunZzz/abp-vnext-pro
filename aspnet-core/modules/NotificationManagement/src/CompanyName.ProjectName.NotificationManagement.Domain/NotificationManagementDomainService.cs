using System;
using Lion.Abp.Domain;

namespace CompanyName.ProjectName.NotificationManagement
{
    public abstract class NotificationManagementDomainService : LionAbpDomainService
    {
        protected NotificationManagementDomainService()
        {
            ObjectMapperContext = typeof(NotificationManagementDomainModule);
        }
    }
}