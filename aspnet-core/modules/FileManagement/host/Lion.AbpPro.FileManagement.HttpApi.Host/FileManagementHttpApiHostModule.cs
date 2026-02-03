using Lion.AbpPro.AspNetCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(FileManagementApplicationModule),
    typeof(FileManagementEntityFrameworkCoreModule),
    typeof(FileManagementHttpApiModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpProAspNetCoreModule)
)]
public class FileManagementHttpApiHostModule : AbpModule
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
            .AddAbpProSwagger("FileManagement");
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
        app.UseAbpProSwaggerUI("/swagger/FileManagement/swagger.json", "FileManagement API");
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}