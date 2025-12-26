using Lion.AbpPro.AspNetCore.Options;
using Volo.Abp.Swashbuckle;

namespace Lion.AbpPro.AspNetCore;

//[DependsOn(typeof(AbpSwashbuckleModule))]
public class AbpProAspNetCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpProConsulOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Consul));
        context.Services.Configure<AbpProCorsOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Cors));
        context.Services.Configure<AbpProMiniProfilerOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.MiniProfiler));
        context.Services.Configure<AbpProMultiTenancyOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.MultiTenancy));
        context.Services.Configure<AbpProSwaggerOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Swagger));
        context.Services.Configure<AbpProAuditOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Audit));
        context.Services.Configure<AbpProJwtOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Jwt));
        context.Services.Configure<AbpProCookieOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Cookie));
        context.Services.Configure<AbpProAntiForgeryOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.AntiForgery));
    }
}