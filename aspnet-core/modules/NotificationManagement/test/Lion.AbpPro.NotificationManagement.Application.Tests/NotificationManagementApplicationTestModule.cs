namespace Lion.AbpPro.NotificationManagement
{
    [DependsOn(
        typeof(NotificationManagementApplicationModule),
        typeof(NotificationManagementDomainTestModule)
        )]
    public class NotificationManagementApplicationTestModule : AbpModule
    {

    }
}
