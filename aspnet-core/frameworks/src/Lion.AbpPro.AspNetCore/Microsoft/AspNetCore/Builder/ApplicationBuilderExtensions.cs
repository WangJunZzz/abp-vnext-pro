using Consul;
using Lion.AbpPro.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Timing;
using System.Threading.Tasks;
using Lion.AbpPro.AspNetCore.Options;
using Microsoft.AspNetCore.Localization;
using Swashbuckle.AspNetCore.SwaggerUI;
using Volo.Abp.Guids;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// consul服务
    /// </summary>
    public static IApplicationBuilder UseAbpProConsul(this IApplicationBuilder app)
    {
        // 获取 AbpProGatewayOptions 配置
        var consulOptions = app.ApplicationServices.GetRequiredService<IOptions<AbpProConsulOptions>>().Value;
        if (!consulOptions.Enabled)
            return app;

        var appLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
        using var scope = app.ApplicationServices.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<IConsulClient>();
        var guidGenerator = scope.ServiceProvider.GetRequiredService<IGuidGenerator>();
        var consulServiceRegistration = new AgentServiceRegistration
        {
            Name = consulOptions.ClientName,
            ID = guidGenerator.Create().ToString(),
            Address = consulOptions.ClientAddress,
            Port = consulOptions.ClientPort,
            Check = new AgentServiceCheck
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(consulOptions.DeregisterCriticalServiceAfter), //服务停止多久后注销
                Interval = TimeSpan.FromSeconds(consulOptions.Interval), //健康检查时间间隔，或者称为心跳 间隔
                HTTP = consulOptions.HealthUrl, //健康检查地址 
                Timeout = TimeSpan.FromSeconds(consulOptions.Timeout) //超时时间
            }
        };

        client.Agent.ServiceRegister(consulServiceRegistration);
        appLifetime.ApplicationStopping.Register(() => { client.Agent.ServiceDeregister(consulServiceRegistration.ID); });
        return app;
    }

    /// <summary>
    /// Ocelot服务
    /// </summary>
    public static IApplicationBuilder UseAbpProOcelot(this IApplicationBuilder app)
    {
        // 获取 AbpProGatewayOptions 配置
        var consulOptions = app.ApplicationServices.GetRequiredService<IOptions<AbpProConsulOptions>>().Value;
        if (!consulOptions.Enabled)
            return app;
        app.UseOcelot().Wait();
        return app;
    }

    /// <summary>
    /// 多语言中间件
    /// <remarks>浏览器传递的请求头：Accept-Language: zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6而abp钟简体中文为：zh-Hans</remarks>
    /// </summary>
    public static IApplicationBuilder UseAbpProRequestLocalization(this IApplicationBuilder app)
    {
        return app.UseAbpRequestLocalization(options =>
        {
            // 移除自带header解析器
            options.RequestCultureProviders.RemoveAll(provider => provider is AcceptLanguageHeaderRequestCultureProvider);
            // 添加header解析器
            options.RequestCultureProviders.Add(new AbpProAcceptLanguageHeaderRequestCultureProvider());
        });
    }

    /// <summary>
    /// SwaggerUI
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static IApplicationBuilder UseAbpProSwaggerUI(this IApplicationBuilder app, string endpoint, string name)
    {
        var swaggerOptions = app.ApplicationServices.GetRequiredService<IOptions<AbpProSwaggerOptions>>().Value;
        if (!swaggerOptions.Enabled) return app;
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(endpoint, name);
            options.DocExpansion(DocExpansion.None);
            options.DefaultModelsExpandDepth(-1);
        });
        return app;
    }
    
    /// <summary>
    /// MiniProfiler
    /// </summary>
    public static IApplicationBuilder UseAbpProMiniProfiler(this IApplicationBuilder app)
    {
        var miniProfilerOptions = app.ApplicationServices.GetRequiredService<IOptions<AbpProMiniProfilerOptions>>().Value;
        if (!miniProfilerOptions.Enabled) return app;
        app.UseMiniProfiler();
        return app;
    }
    
    /// <summary>
    /// MultiTenancy
    /// </summary>
    public static IApplicationBuilder UseAbpProMultiTenancy(this IApplicationBuilder app)
    {
        var abpProMultiTenancyOptions = app.ApplicationServices.GetRequiredService<IOptions<AbpProMultiTenancyOptions>>().Value;
        if (!abpProMultiTenancyOptions.Enabled) return app;
        app.UseMultiTenancy();
        return app;
    }
    
    /// <summary>
    /// 跨域设置
    /// </summary>
    public static IApplicationBuilder UseAbpProCors(this IApplicationBuilder app)
    {
        var corsOptions = app.ApplicationServices.GetRequiredService<IOptions<AbpProCorsOptions>>().Value;
        if (!corsOptions.Enabled) return app;
        app.UseCors(AbpProAspNetCoreConsts.DefaultCorsPolicyName);
        return app;
    }
    
    /// <summary>
    /// 审计日志
    /// </summary>
    public static IApplicationBuilder UseAbpProAuditing(this IApplicationBuilder app)
    {
        var auditOptions = app.ApplicationServices.GetRequiredService<IOptions<AbpProAuditOptions>>().Value;
        if (!auditOptions.Enabled) return app;
        app.UseAuditing();
        return app;
    }
}