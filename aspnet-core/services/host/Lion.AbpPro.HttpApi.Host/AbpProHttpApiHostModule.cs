namespace Lion.AbpPro
{
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
        typeof(AbpProStarterModule)
        //typeof(AbpBackgroundJobsHangfireModule)
    )]
    public partial class AbpProHttpApiHostModule : AbpModule
    {
        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用程序初始化的时候注册hangfire
            //context.CreateRecurringJob();
            base.OnPostApplicationInitialization(context);
        }

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
                .AddAbpProSwagger("AbpPro");


            // //ConfigureCache(context);
            // context.Services.AddAbpProRedis();
            //
            // //ConfigurationDistributedLocking(context);
            // context.Services.AddAbpProRedisDistributedLocking();
            //
            //
            // //ConfigureSwaggerServices(context);
            // context.Services.AddAbpProSwagger("AbpPro");
            //
            // //ConfigureJwtAuthentication(context, configuration);
            // context.Services.AddAbpProAuthentication();
            //
            // //ConfigureHangfire(context);
            //
            // //ConfigureMiniProfiler(context);
            // context.Services.AddAbpProMiniProfiler();
            //
            // //ConfigureIdentity(context);
            // context.Services.AddAbpProIdentity();
            //
            // //ConfigureCap(context);
            //
            // //ConfigureAuditLog(context);
            // context.Services.AddAbpProAuditLog();
            //
            // //ConfigurationSignalR(context);
            // context.Services.AddAbpProSignalR();
            //
            // //ConfigurationMultiTenancy();
            // context.Services.AddAbpProMultiTenancy();
            //
            // //ConfigureBlobStorage();
            // context.Services.AddAbpProBlobStorage();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            //var configuration = context.GetConfiguration();
            app.UseAbpProRequestLocalization();
            app.UseCorrelationId();
            app.MapAbpStaticAssets();
            // if (configuration.GetValue("MiniProfiler:Enabled", false))
            // {
            //     app.UseMiniProfiler();
            // }

            app.UseAbpProMiniProfiler();
            app.UseRouting();
            //app.UseCors(AbpProHttpApiHostConst.DefaultCorsPolicyName);
            app.UseAbpProCors();
            app.UseAuthentication();

            // if (MultiTenancyConsts.IsEnabled)
            // {
            //     app.UseMultiTenancy();
            // }

            app.UseAbpProMultiTenancy();

            app.UseAuthorization();
            // app.UseSwagger();
            // app.UseAbpSwaggerUI(options =>
            // {
            //     options.SwaggerEndpoint("/swagger/AbpPro/swagger.json", "AbpPro API");
            //     options.DocExpansion(DocExpansion.None);
            //     options.DefaultModelsExpandDepth(-1);
            // });

            app.UseAbpProSwaggerUI("/swagger/AbpPro/swagger.json","AbpPro");
            //app.UseAuditing();
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