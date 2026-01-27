using Lion.AbpPro.AspNetCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace Lion.AbpPro.NotificationManagement;

[DependsOn(
    typeof(NotificationManagementApplicationModule),
    typeof(NotificationManagementEntityFrameworkCoreModule),
    typeof(NotificationManagementHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpAspNetCoreSerilogModule),
    
    typeof(AbpProCapModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpProAspNetCoreModule)
)]
public class NotificationManagementHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services
            .AddAbpProAuditLog()
            .AddAbpProAuthentication()
            .AddAbpProMultiTenancy()
            .AddAbpProHealthChecks()
            .AddAbpProTenantResolvers()
            .AddAbpProLocalization()
            .AddAbpProExceptions()
            .AddAbpProSwagger("NotificationManagement");
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
            options.SwaggerEndpoint("/swagger/NotificationManagement/swagger.json", "NotificationManagement API");
            options.DocExpansion(DocExpansion.None);
            options.DefaultModelsExpandDepth(-1);
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}