using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace Lion.AbpPro.BasicManagement;

[DependsOn(
    typeof(AbpProAspNetCoreModule),
    typeof(BasicManagementApplicationModule),
    typeof(BasicManagementEntityFrameworkCoreModule),
    typeof(BasicManagementHttpApiModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class BasicManagementHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services
            .AddAbpProAuditLog()
            .AddAbpProJwtBearer()
            .AddAbpProMultiTenancy()
            .AddAbpProHealthChecks()
            .AddAbpProTenantResolvers()
            .AddAbpProLocalization()
            .AddAbpProExceptions()
            .AddAbpProSwagger("BasicManagement");
        Configure<AbpDbContextOptions>(options => { options.UseNpgsql(); });
        context.Services.AddAlwaysAllowAuthorization();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseMultiTenancy();
        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UseAbpProSwaggerUI("/swagger/BasicManagement/swagger.json", "BasicManagement API");
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}