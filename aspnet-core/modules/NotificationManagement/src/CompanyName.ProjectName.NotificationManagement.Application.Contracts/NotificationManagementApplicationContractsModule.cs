using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace CompanyName.ProjectName.NotificationManagement
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
