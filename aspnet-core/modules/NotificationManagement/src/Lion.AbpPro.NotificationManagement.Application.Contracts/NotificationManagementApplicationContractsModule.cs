using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Lion.AbpPro.NotificationManagement
{
    [DependsOn(
        typeof(NotificationManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class NotificationManagementApplicationContractsModule : AbpModule
    {

    }
}
