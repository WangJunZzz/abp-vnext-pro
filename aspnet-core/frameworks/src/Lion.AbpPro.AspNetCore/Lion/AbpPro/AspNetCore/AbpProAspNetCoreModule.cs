using Lion.AbpPro.AspNetCore.Options;

namespace Lion.AbpPro.AspNetCore;

public class AbpProAspNetCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpProGatewayOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Gateway));
        context.Services.Configure<AbpProCorsOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Cors));
        context.Services.Configure<AbpProMiniProfilerOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.MiniProfiler));
        context.Services.Configure<AbpProMultiTenancyOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.MultiTenancy));
        context.Services.Configure<AbpProSwaggerOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Swagger));
        context.Services.Configure<AbpProAuditOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Audit));
        context.Services.Configure<AbpProJwtOptions>(context.Configuration.GetSection(AbpProAspNetCoreConsts.Jwt));
    }
}