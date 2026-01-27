using Lion.AbpPro.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace Lion.AbpPro.LanguageManagement
{
    [DependsOn(
        typeof(LanguageManagementApplicationModule),
        typeof(LanguageManagementEntityFrameworkCoreModule),
        typeof(LanguageManagementHttpApiModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSerilogModule),
        
        typeof(AbpProAspNetCoreModule)
    )]
    public class LanguageManagementHttpApiHostModule : AbpModule
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
                .AddAbpProSwagger("LanguageManagement");
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
                options.SwaggerEndpoint("/swagger/LanguageManagement/swagger.json", "LanguageManagement API");
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}