namespace Lion.AbpPro;

[DependsOn(
    typeof(AbpProHttpApiModule),
    typeof(AbpProAspNetCoreModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpProEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAccountWebModule),
    typeof(AbpProApplicationModule),
    // typeof(AbpProCapModule),
    // typeof(AbpProCapEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpBlobStoringFileSystemModule),
    typeof(AbpProStarterModule),
    typeof(AbpSwashbuckleModule)
    //typeof(AbpBackgroundJobsHangfireModule)
)]
public partial class AbpProHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services
            .AddAbpProAuditLog()
            .AddAbpProAuthentication()
            .AddAbpProMultiTenancy()
            .AddAbpProRedis()
            .AddAbpProRedisDistributedLocking()
            .AddAbpProMiniProfiler()
            .AddAbpProCors()
            .AddAbpProAntiForgery()
            .AddAbpProIdentity()
            .AddAbpProBlobStorage()
            .AddAbpProSignalR()
            .AddAbpProHealthChecks()
            .AddAbpProTenantResolvers()
            .AddAbpProLocalization()
            .AddAbpProExceptions()
            .AddAbpProConsul()
            .AddAbpProSwagger("AbpPro");
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseAbpProRequestLocalization();
        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseAbpProMiniProfiler();
        app.UseRouting();
        app.UseAbpProCors();
        app.UseAuthentication();
        app.UseAbpProMultiTenancy();
        app.UseAuthorization();
        app.UseAbpProSwaggerUI("/swagger/AbpPro/swagger.json","AbpPro");
        app.UseAbpProAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health"); 
                
            // endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions()
            // {
            //     Authorization = new[] { new CustomHangfireAuthorizeFilter() },
            //     IgnoreAntiforgeryToken = true
            // });

        });
        app.UseAbpProConsul();
    }
}