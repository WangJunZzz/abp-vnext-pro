using MyCompanyName.MyProjectName.MyModuleName;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{

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
        service.Configure<AbpDbContextOptions>(options => { options.UseNpgsql(builder => { builder.TranslateParameterizedCollectionsToConstants(); }); });
        return service;
    }

    
}