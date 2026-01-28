using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace Lion.AbpPro.BasicManagement;

[DependsOn(
    typeof(BasicManagementApplicationModule),
    typeof(BasicManagementEntityFrameworkCoreModule),
    typeof(BasicManagementHttpApiModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpAspNetCoreSerilogModule),
    
    typeof(AbpProAspNetCoreModule)
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
            options.SwaggerEndpoint("/swagger/BasicManagement/swagger.json", "BasicManagement API");
            options.DocExpansion(DocExpansion.None);
            options.DefaultModelsExpandDepth(-1);
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}