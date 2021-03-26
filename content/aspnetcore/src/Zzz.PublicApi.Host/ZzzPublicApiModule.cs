using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Zzz.Extensions;
using Zzz.PublicApi.Host;

namespace Zzz.PublicApi
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(ZzzHttpApiClientModule)
    )]
    public class ZzzPublicApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureSwagger(context);
            ConfigureAuthentication(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zzz.PublicApi.Host v1"));
            }

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = SerilogToEsExtensions.EnrichFromRequest;
            });
            app.UseConfiguredEndpoints();
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            IdentityModelEventSource.ShowPII = true;
            context.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    //token颁发者
                    options.Authority = configuration["AuthServer:Authority"];
                    options.Audience = configuration["AuthServer:ApiName"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                });

            context.Services.AddAuthorization(options =>
            {
                // 为了掩饰 分读写权限 
                options.AddPolicy(ZzzPublicApiConsts.Policy_Read, builder =>
                            builder.RequireScope(new string[] {
                                ZzzPublicApiConsts.Scope_Read })
                       );

                options.AddPolicy(ZzzPublicApiConsts.Policy_Write, builder =>
                            builder.RequireScope(new string[] {
                                ZzzPublicApiConsts.Scope_Write })
                       );
            });
        }

        private void ConfigureSwagger(ServiceConfigurationContext context)
        {
            var services = context.Services;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zzz.PublicApi.Host", Version = "v1" });
            });
        }
    }
}
