using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
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
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<NotificationManagementApplicationModule>(validate: true); });

            ConfigurationSignalR(context);
        }

        private void ConfigurationSignalR(ServiceConfigurationContext context)
        {
            var redisConnection = context.Services.GetConfiguration()["Redis:Configuration"];
            var redisConnectionStringList = redisConnection.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (redisConnectionStringList == null || redisConnectionStringList.Length == 0)
            {
                throw new UserFriendlyException(message: "Redis连接字符串配置异常");
            }

            var password = string.Empty;
            if (redisConnection.Contains("password"))
            {
                password = redisConnectionStringList[1].Split('=')[1];
            }

            var redisDatabaseId = 0;
            if (redisConnection.Contains("defaultdatabase"))
            {
                redisDatabaseId = Convert.ToInt32(redisConnectionStringList[2].Split('=')[1]);
            }


            context.Services.AddSignalR().AddStackExchangeRedis(options =>
            {
                options.Configuration.ChannelPrefix = "CompanyName.ProjectName";
                options.Configuration.DefaultDatabase = redisDatabaseId;
                options.Configuration.Password = password;
                options.Configuration.EndPoints.Add(redisConnectionStringList[0]);
            });
        }
    }
}