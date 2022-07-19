namespace Lion.AbpPro.WebGateway
{
    [DependsOn(
        typeof(SharedHostingGatewayModule))]
    public class WebGatewayModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureCors(context);
          
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseCorrelationId();
            app.UseCors(DefaultCorsPolicyName);
            app.UseRouting();
            app.UseConfiguredEndpoints(endpoints => { endpoints.MapHealthChecks("/health"); });
            app.UseWebSockets();
            app.UseOcelot().Wait();
        }

        /// <summary>
        /// 配置跨域
        /// </summary>
        private void ConfigureCors(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
     
    }
}