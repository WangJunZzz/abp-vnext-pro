using Lion.AbpPro.AspNetCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace Lion.AbpPro.FileManagement;

[DependsOn(
    typeof(FileManagementApplicationModule),
    typeof(FileManagementEntityFrameworkCoreModule),
    typeof(FileManagementHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
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
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/FileManagement/swagger.json", "FileManagement API");
            options.DocExpansion(DocExpansion.None);
            options.DefaultModelsExpandDepth(-1);
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}