using Hangfire;
using Hangfire.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;
using Zzz.Options;

namespace Zzz
{
    [DependsOn(
        typeof(ZzzApplicationModule),
        typeof(ZzzDomainTestModule),
        typeof(ZzzApplicationContractsModule),
        typeof(AbpBackgroundJobsHangfireModule)
        )]
    public class ZzzApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureHangfire(context.Services);
        }

        /// <summary>
        /// 注入Hangfire服务
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureHangfire(IServiceCollection services)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });

            var redisConnectionString = services.GetConfiguration().GetSection("Cache:Redis:ConnectionString").Value;
            var redisDatabaseId = Convert.ToInt32(services.GetConfiguration().GetSection("Cache:Redis:DatabaseId").Value);

            // 启用Hangfire 并使用Redis作为持久化
            services.AddHangfire(config =>
            {
                config.UseRedisStorage(redisConnectionString, new RedisStorageOptions { Db = redisDatabaseId });
            });

            JobStorage.Current = new RedisStorage(redisConnectionString, new RedisStorageOptions { Db = redisDatabaseId });

        }
    }
}