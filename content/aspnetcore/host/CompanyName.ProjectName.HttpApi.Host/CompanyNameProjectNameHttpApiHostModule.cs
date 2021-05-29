using CompanyNameProjectName.EntityFrameworkCore;
using CompanyNameProjectName.Extensions;
using CompanyNameProjectName.Extensions.Filters;
using CompanyNameProjectName.Options;
using Hangfire;
using Hangfire.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace CompanyNameProjectName
{
    [DependsOn(
        typeof(CompanyNameProjectNameHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(CompanyNameProjectNameApplicationModule),
        typeof(CompanyNameProjectNameEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpBackgroundJobsHangfireModule)
    )]
    public class CompanyNameProjectNameHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用程序初始化的时候注册hangfire
            var app = context.GetApplicationBuilder();
            app.ApplicationServices.GetService<ISettingDefinitionManager>().Get(LocalizationSettingNames.DefaultLanguage).DefaultValue = "zh-Hans";
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new CustomHangfireAuthorizeFilter() }
            });
            // 定时任务
            //context.ServiceProvider.CreateRecurringJob();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            ConfigureOptions(context);
            ConfigureBundles();
            ConfigureUrls(configuration);
            ConfigureConventionalControllers();
            ConfigureJwtAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureVirtualFileSystem(context);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
            ConfigureAbpExcepotions(context);
            ConfigureCache(context.Services);
            ConfigureAuditLog();
            ConfigureHangfire(context.Services);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseAbpSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CompanyNameProjectName API");
                c.DefaultModelExpandDepth(-2);
                c.DocExpansion(DocExpansion.None);
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = SerilogToEsExtensions.EnrichFromRequest;
            });
            app.UseConfiguredEndpoints();


        }

        #region 私有方法
        /// <summary>
        /// 配置options
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureOptions(ServiceConfigurationContext context)
        {
            context.Services.Configure<JwtOptions>(context.Services.GetConfiguration().GetSection("Jwt"));
        }


        /// <summary>
        /// 审计日志
        /// </summary>
        private void ConfigureAuditLog()
        {
            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabled = true; 
                options.EntityHistorySelectors.AddAllEntities(); 
                options.ApplicationName = "CompanyName.ProjectName";
            });
        }

        /// <summary>
        /// Redis缓存
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureCache(IServiceCollection services)
        {
            var redisConnectionString = services.GetConfiguration().GetSection("Cache:Redis:ConnectionString").Value;
            var redisDatabaseId = Convert.ToInt32(services.GetConfiguration().GetSection("Cache:Redis:DatabaseId").Value);
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString + ",defaultdatabase=" + redisDatabaseId;
            });
        }


        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureAbpExcepotions(ServiceConfigurationContext context)
        {
            // dev环境显示异常具体信息
            if (context.Services.GetHostingEnvironment().IsDevelopment())
            {
                context.Services.Configure<AbpExceptionHandlingOptions>(options =>
                {
                    options.SendExceptionsDetailsToClients = true;
                });
            }
        }


        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                options.StyleBundles.Configure(
                    BasicThemeBundles.Styles.Global,
                    bundle => { bundle.AddFiles("/global-styles.css"); }
                );
            });
        }


        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        /// <summary>
        /// 配置虚拟文件系统
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CompanyNameProjectNameHttpApiHostModule>();
            });
        }

        /// <summary>
        /// 映射Controller
        /// </summary>
        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(CompanyNameProjectNameApplicationModule).Assembly);
            });
       
        }

        /// <summary>
        /// 配置JWT
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureJwtAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        // 是否开启签名认证
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        //ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecurityKey"]))
                    };
                });
        }

        /// <summary>
        /// 配置SwaggerUI
        /// </summary>
        /// <param name="context"></param>
        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CompanyNameProjectName API", Version = "v1" });

                    options.DocInclusionPredicate((docName, description) => true);
                    options.EnableAnnotations();// 启用注解
                    // 加载xml文件，不然不会显示备注
                    //var xmlapppath = Path.Combine(AppContext.BaseDirectory, "CompanyNameProjectName.Application.xml");
                    //var xmlContractspath = Path.Combine(AppContext.BaseDirectory, "CompanyNameProjectName.Application.Contracts.xml");
                    //var xmlapipath = Path.Combine(AppContext.BaseDirectory, "CompanyNameProjectName.HttpApi.Host.xml");
                    //options.IncludeXmlComments(xmlapppath, true);
                    //options.IncludeXmlComments(xmlContractspath, true);
                    //options.IncludeXmlComments(xmlapipath, true);

                    //options.OperationFilter<SwaggerTagsFilter>();
                    options.DocumentFilter<HiddenAbpDefaultApiFilter>();
                    // 在swaggerui界面添加token认证
                    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                    {
                        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT"
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"
                            }
                        },
                        new List<string>()
                        }
                    });
                });
        }

        /// <summary>
        ///配置本地化
        /// </summary>
        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            });
        }

        /// <summary>
        /// 配置跨域
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
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


        /// <summary>
        /// 配置Hangfire服务
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureHangfire(IServiceCollection services)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = true;
            });

            var redisConnectionString = services.GetConfiguration().GetSection("Cache:Redis:ConnectionString").Value;
            var redisDatabaseId = Convert.ToInt32(services.GetConfiguration().GetSection("Cache:Redis:DatabaseId").Value);

            // 启用Hangfire 并使用Redis作为持久化
            services.AddHangfire(config =>
            {
                config.UseRedisStorage(redisConnectionString, new RedisStorageOptions { Db = redisDatabaseId });
            });

            JobStorage.Current = new RedisStorage(redisConnectionString, new RedisStorageOptions { Db = redisDatabaseId });
        }
        #endregion

    }
}
