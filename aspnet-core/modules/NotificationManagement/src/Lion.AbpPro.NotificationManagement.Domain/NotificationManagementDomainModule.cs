using Lion.AbpPro.SignalR;

namespace Lion.AbpPro.NotificationManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NotificationManagementDomainSharedModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpProSignalRModule)
    )]
    public class NotificationManagementDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NotificationManagementDomainModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<NotificationDomainAutoMapperProfile>(validate: true);
            });
        }
    }
}
