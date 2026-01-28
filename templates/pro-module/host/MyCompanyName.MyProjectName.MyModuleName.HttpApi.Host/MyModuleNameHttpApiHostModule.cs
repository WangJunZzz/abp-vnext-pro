using Lion.AbpPro.AspNetCore;

namespace MyCompanyName.MyProjectName.MyModuleName;

[DependsOn(
    typeof(AbpProAspNetCoreModule),
    typeof(MyModuleNameApplicationModule),
    typeof(MyModuleNameEntityFrameworkCoreModule),
    typeof(MyModuleNameHttpApiModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class MyModuleNameHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
         AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpProSwagger("MyProjectName")
            .AddAbpProCors()
            .AddAbpProLocalization()
            .AddAbpProExceptions()
            .AddAbpProHealthChecks()
            .AddAbpProTenantResolvers()
            .AddAbpProMultiTenancy()
            .AddAbpProAntiForgery()
            .AddAbpProVirtualFileSystem()
            .AddAbpProDbContext()
            .AddAlwaysAllowAuthorization();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseAbpProRequestLocalization();
        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseRouting();
        app.UseAbpProCors();
        app.UseAuthentication();
        app.UseAbpProMultiTenancy();
        app.UseAuthorization();
        app.UseAbpProSwaggerUI("/swagger/MyProjectName/swagger.json", "MyProjectName");
        app.UseAbpSerilogEnrichers();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints(endpoints => { endpoints.MapHealthChecks("/health"); });
    }
}