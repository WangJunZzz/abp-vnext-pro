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
            ConfigurationSignalR(context);
        }

        private void ConfigurationSignalR(ServiceConfigurationContext context)
        {
            var redisConnection = context.Services.GetConfiguration()["Redis:Configuration"];

            if (redisConnection.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException(message: "Redis连接字符串未配置.");
            }

            context.Services.AddSignalR().AddStackExchangeRedis(redisConnection, options => { options.Configuration.ChannelPrefix = "Lion.AbpPro"; });
        }
    }
}