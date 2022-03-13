using Lion.AbpPro.AuditLoggingManagement.Domain.Shared.Localization;
using Volo.Abp.Application.Services;

namespace Lion.AbpPro.AuditLoggingManagement
{
    public abstract class AuditLoggingManagementAppServiceBase : ApplicationService
    {
        protected AuditLoggingManagementAppServiceBase()
        {
            LocalizationResource = typeof(AuditLogManagementResource);
            ObjectMapperContext = typeof(AuditLoggingManagementApplicationModule);
        }
    }
}