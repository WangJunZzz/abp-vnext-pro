namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameHttpApiModule),
        typeof(AbpProAspNetCoreModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(MyProjectNameEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAccountWebModule),
        typeof(MyProjectNameApplicationModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpCachingStackExchangeRedisModule)
    )]
    public class MyProjectNameHttpApiHostModule : AbpModule
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
                .AddAbpProSignalR()
                .AddAbpProHealthChecks()
                .AddAbpProTenantResolvers()
                .AddAbpProLocalization()
                .AddAbpProExceptions()
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
}