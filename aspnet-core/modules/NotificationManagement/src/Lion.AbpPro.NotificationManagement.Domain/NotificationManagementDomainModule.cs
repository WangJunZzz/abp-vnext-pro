using Lion.AbpPro.SignalR;

namespace Lion.AbpPro.NotificationManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NotificationManagementDomainSharedModule),
        typeof(AbpProSignalRModule)
    )]
    public class NotificationManagementDomainModule : AbpModule
    {
    }
}
