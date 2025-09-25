using Microsoft.AspNetCore.Identity;
using MyCompanyName.MyProjectName.MyModuleName;

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
    /// 配置虚拟文件系统
    /// </summary>
    public static IServiceCollection AddAbpProVirtualFileSystem(this IServiceCollection service)
    {
        service.Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MyModuleNameHttpApiHostModule>();
        });
        return service;
    }
    
    public static IServiceCollection AddAbpProDbContext(this IServiceCollection service)
    {
        service.Configure<AbpDbContextOptions>(options => { options.UseMySQL(builder => { builder.TranslateParameterizedCollectionsToConstants(); }); });
        return service;
    }

    
}