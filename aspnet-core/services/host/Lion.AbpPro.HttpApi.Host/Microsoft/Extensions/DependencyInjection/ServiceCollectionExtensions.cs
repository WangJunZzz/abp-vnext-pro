using Lion.AbpPro.Hangfire;

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

    /// <summary>
    /// 注册blob设置
    /// </summary>
    public static IServiceCollection AddAbpProBlobStorage(this IServiceCollection service)
    {
        service.Configure<AbpBlobStoringOptions>(options => { options.Containers.ConfigureDefault(container => { container.UseFileSystem(fileSystem => { fileSystem.BasePath = "C:\\my-files"; }); }); });
        return service;
    }

    /// <summary>
    /// 注册cap
    /// </summary>
    public static IServiceCollection AddAbpProCap(this IServiceCollection service)
    {
        var configuration = service.GetConfiguration();
        service.AddAbpCap(capOptions =>
        {
            capOptions.SetCapDbConnectionString(configuration["ConnectionStrings:Default"]);
            capOptions.UseEntityFramework<AbpProDbContext>();
            capOptions.UseRabbitMQ(option =>
            {
                option.HostName = configuration.GetValue<string>("Cap:RabbitMq:HostName");
                option.UserName = configuration.GetValue<string>("Cap:RabbitMq:UserName");
                option.Password = configuration.GetValue<string>("Cap:RabbitMq:Password");
                option.Port = configuration.GetValue<int>("Cap:RabbitMq:Port");
            });
            capOptions.UseDashboard(options => { options.AuthorizationPolicy = AbpProCapPermissions.CapManagement.Cap; });
        });
        return service;
    }

    /// <summary>
    /// 注册hangfire
    /// </summary>
    public static IServiceCollection AddAbpProHangfire(this IServiceCollection service)
    {
        var redisStorageOptions = new RedisStorageOptions()
        {
            Db = service.GetConfiguration().GetValue<int>("Hangfire:Redis:DB")
        };

        service.Configure<AbpBackgroundJobOptions>(options => { options.IsJobExecutionEnabled = true; });

        service.AddHangfire(config =>
        {
            config.UseRedisStorage(service.GetConfiguration().GetValue<string>("Hangfire:Redis:Host"), redisStorageOptions)
                .WithJobExpirationTimeout(TimeSpan.FromDays(7));
            var delaysInSeconds = new[] { 10, 60, 60 * 3 }; // 重试时间间隔
            const int attempts = 3; // 重试次数
            config.UseFilter(new AutomaticRetryAttribute() { Attempts = 3, DelaysInSeconds = delaysInSeconds });
            //config.UseFilter(new AutoDeleteAfterSuccessAttribute(TimeSpan.FromDays(7)));
            //config.UseFilter(new JobRetryLastFilter(attempts));
        });
        return service;
    }
}