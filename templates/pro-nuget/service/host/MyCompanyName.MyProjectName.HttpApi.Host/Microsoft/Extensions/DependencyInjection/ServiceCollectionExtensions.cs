using Medallion.Threading;
using Medallion.Threading.Redis;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注册Redis缓存
    /// </summary>
    public static IServiceCollection AddAbpProRedis(this IServiceCollection service)
    {
        service.Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "AbpPro:"; });
        var configuration = service.GetConfiguration();
        var redis = ConnectionMultiplexer.Connect(configuration.GetValue<string>("Redis:Configuration"));
        service
            .AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "AbpPro-Protection-Keys");
        return service;
    }

    /// <summary>
    /// 注册redis分布式锁
    /// </summary>
    public static IServiceCollection AddAbpProRedisDistributedLocking(this IServiceCollection service)
    {
        var configuration = service.GetConfiguration();
        var connectionString = configuration.GetValue<string>("Redis:Configuration");
        service.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = ConnectionMultiplexer.Connect(connectionString);
            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
        });
        return service;
    }

    /// <summary>
    /// 注册Identity
    /// </summary>
    public static IServiceCollection AddAbpProIdentity(this IServiceCollection service)
    {
        service.Configure<IdentityOptions>(options => { options.Lockout = new LockoutOptions() { AllowedForNewUsers = false }; });
        return service;
    }

    /// <summary>
    /// 注册SignalR
    /// </summary>
    public static IServiceCollection AddAbpProSignalR(this IServiceCollection service)
    {
        service
            .AddSignalR()
            .AddStackExchangeRedis(service.GetConfiguration().GetValue<string>("Redis:Configuration"),
                options => { options.Configuration.ChannelPrefix = "Lion.AbpPro"; });
        return service;
    }
}