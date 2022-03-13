using Lion.AbpPro.AuditLoggingManagement.Domain.Shared;
using Volo.Abp.Application;
using Volo.Abp.AuditLogging;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Lion.AbpPro.AuditLoggingManagement
{
    [DependsOn(
        typeof(AuditLoggManagementDomainSharedModule),
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationAbstractionsModule)
    )]
    public class AuditLoggingManagementApplicationContractsModule : AbpModule
    {
    }
}
