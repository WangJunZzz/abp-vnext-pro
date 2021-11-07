using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.ProjectName.Shared.Hosting.Gateways;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.SwaggerUI;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.WebGateway
{
    [DependsOn(
        typeof(SharedHostingGatewayModule))]
    public class WebGatewayModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureCors(context);
            ConfigureSwaggerServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            app.UseCorrelationId();
            app.UseCors(DefaultCorsPolicyName);
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/ProjectName/swagger.json", "ProjectNameAPI");
                options.DefaultModelsExpandDepth(-1);
                options.DocExpansion(DocExpansion.None);
            });
            
            app.UseConfiguredEndpoints();
            app.UseWebSockets();
            app.UseOcelot().Wait();
        }

        /// <summary>
        /// 配置跨域
        /// </summary>
        /// <param name="context"></param>
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
        
        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "WebGateway API", Version = "v1"});
                    options.DocInclusionPredicate((docName, description) => true);
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    });
                    
                });
        }
    }
}