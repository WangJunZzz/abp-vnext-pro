namespace Lion.AbpPro.NotificationManagement
{
    [DependsOn(
        typeof(NotificationManagementDomainModule),
        typeof(NotificationManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAspNetCoreSignalRModule)
    )]
    public class NotificationManagementApplicationModule : AbpModule
    {
    }
}