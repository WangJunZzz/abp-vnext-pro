namespace Lion.AbpPro.NotificationManagement
{
    [DependsOn(
        typeof(NotificationManagementDomainModule),
        typeof(NotificationManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreSignalRModule)
    )]
    public class NotificationManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NotificationManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<NotificationManagementApplicationModule>(validate: true); });
        }
    }
}