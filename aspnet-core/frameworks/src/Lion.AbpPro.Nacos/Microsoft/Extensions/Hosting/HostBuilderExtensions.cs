using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting;

public static class HostBuilderExtensions
{
    /// <summary>
    /// 添加Nacos配置支持
    /// </summary>
    /// <param name="hostBuilder">主机构建器</param>
    /// <returns>主机构建器</returns>
    public static IHostBuilder UseNacos(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            var configuration = builder.Build();
            // 从配置文件读取Nacos相关配置
            // 默认会使用JSON解析器来解析存在Nacos Server的配置
            builder.AddNacosV2Configuration(configuration.GetSection("NacosConfig"));
        });
    }
    
    /// <summary>
    /// 添加Nacos配置支持
    /// </summary>
    /// <param name="hostBuilder">主机构建器</param>
    /// <param name="sectionName">Nacos配置节名称，默认为"NacosConfig"</param>
    /// <returns>主机构建器</returns>
    public static IHostBuilder UseNacos(this IHostBuilder hostBuilder, string sectionName = "NacosConfig")
    {
        return hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            var configuration = builder.Build();
            // 从配置文件读取Nacos相关配置
            builder.AddNacosV2Configuration(configuration.GetSection(sectionName));
        });
    }
}