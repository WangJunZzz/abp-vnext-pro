using Lion.AbpPro.AspNetCore;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace Lion.AbpPro.DataDictionaryManagement
{
    [DependsOn(
        typeof(DataDictionaryManagementApplicationModule),
        typeof(DataDictionaryManagementEntityFrameworkCoreModule),
        typeof(DataDictionaryManagementHttpApiModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule),
        typeof(AbpProAspNetCoreModule)
    )]
    public class DataDictionaryManagementHttpApiHostModule : AbpModule
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
                .AddAbpProSwagger("DataDictionaryManagement");
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
            app.UseAbpProSwaggerUI("/swagger/DataDictionaryManagement/swagger.json", "DataDictionaryManagement API");
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}