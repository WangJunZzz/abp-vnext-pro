using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.Extensions.Configuration;
using Volo.Abp.AspNetCore.SignalR;

namespace CompanyName.ProjectName.NotificationManagement
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
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<NotificationManagementApplicationModule>(validate: true);
            });

            ConfigurationSignalR(context);
        }
        
        private void ConfigurationSignalR(ServiceConfigurationContext context)
        {
            var redisConnectionString =
                context.Services.GetConfiguration().GetSection("Cache:Redis:ConnectionString").Value;
            var redisDatabaseId = context.Services.GetConfiguration().GetValue<int>("Cache:Redis:DatabaseId");
            var password = context.Services.GetConfiguration().GetValue<string>("Cache:Redis:Password");
            context.Services.AddSignalR().AddStackExchangeRedis(options =>
            {
                options.Configuration.ChannelPrefix = "CompanyName.ProjectName";
                options.Configuration.DefaultDatabase = redisDatabaseId;
                options.Configuration.Password = password;
                options.Configuration.EndPoints.Add(redisConnectionString);
            });
        }
    }
}
