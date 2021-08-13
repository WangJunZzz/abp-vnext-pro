using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.NotificationManagement
{
    [DependsOn(
        typeof(NotificationManagementApplicationModule),
        typeof(NotificationManagementDomainTestModule)
        )]
    public class NotificationManagementApplicationTestModule : AbpModule
    {

    }
}
