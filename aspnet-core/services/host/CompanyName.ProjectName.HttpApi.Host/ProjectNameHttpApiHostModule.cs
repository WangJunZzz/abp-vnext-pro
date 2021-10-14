using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyName.ProjectName.ConfigurationOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CompanyName.ProjectName.EntityFrameworkCore;
using CompanyName.ProjectName.Extensions.Filters;
using CompanyNameProjectName.Extensions.Filters;
using Hangfire;
using Hangfire.MySql;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;
using System.Threading.Tasks;
using CompanyName.ProjectName.CAP;
using CompanyName.ProjectName.Extensions;
using CompanyName.ProjectName.Extensions.Customs.Http;
using CompanyName.ProjectName.MultiTenancy;
using Savorboard.CAP.InMemoryMessageQueue;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Caching;

namespace CompanyName.ProjectName
{
    [DependsOn(
        typeof(ProjectNameHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(ProjectNameApplicationModule),
        typeof(ProjectNameEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAccountWebModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(AbpCapModule),
        typeof(AbpAspNetCoreMultiTenancyModule)
    )]
    public class ProjectNameHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            // context.CreateRecurringJob();
            base.OnPostApplicationInitialization(context);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            //ConfigureConventionalControllers();
            //ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureCache(context);
            ConfigureVirtualFileSystem(context);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context, configuration);
            ConfigureOptions(context);
            ConfigureHealthChecks(context);
            ConfigureJwtAuthentication(context, configuration);
            ConfigureHangfireMysql(context);
            ConfigurationCap(context);
            ConfigurationStsHttpClient(context);
            ConfigureAbpExceptions(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseAbpRequestLocalization();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            
            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }
        
            app.UseAuthorization();

            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectName API");
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseSerilogRequestLogging(opts => { opts.EnrichDiagnosticContext = SerilogToEsExtensions.EnrichFromRequest; });
            app.UseUnitOfWork();
            app.UseConfiguredEndpoints();
            app.UseEndpoints(endpoints => { endpoints.MapHealthChecks("/health"); });
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] {new CustomHangfireAuthorizeFilter()},
                IgnoreAntiforgeryToken = true
            });
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureAbpExceptions(ServiceConfigurationContext context)
        {
            context.Services.Configure<AbpExceptionHandlingOptions>(options => { options.SendExceptionsDetailsToClients = true; });
        }

        public void ConfigureHangfireMysql(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => { options.IsJobExecutionEnabled = true; });
            context.Services.AddHangfire(config =>
            {
                config.UseStorage(new MySqlStorage(context.Services.GetConfiguration().GetConnectionString("Default"),
                    new MySqlStorageOptions()
                    {
                        //CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        //SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        //QueuePollInterval = TimeSpan.Zero,
                        //UseRecommendedIsolationLevel = true,
                        //DisableGlobalLocks = true
                    }));
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
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecurityKey"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = currentContext =>
                        {
                            var accessToken = currentContext.Request.Query["access_token"];
                            // 如果请求来自signalr
                            var path = currentContext.HttpContext.Request.Path;
                            if (path.StartsWithSegments("/signalr"))
                            {
                                currentContext.Token = accessToken;
                            }

                            // 如果请求来自hangfire 或者cap
                            if (path.ToString().StartsWith("/hangfire") || path.ToString().StartsWith("/cap"))
                            {
                                currentContext.HttpContext.Response.Headers.Remove("X-Frame-Options");
                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    currentContext.Token = accessToken;
                                    currentContext.HttpContext.Response.Cookies
                                        .Append("ProjectNameCookie", accessToken);
                                }
                                else
                                {
                                    var cookies = currentContext.Request.Cookies;
                                    if (cookies.ContainsKey("ProjectNameCookie"))
                                    {
                                        currentContext.Token = cookies["ProjectNameCookie"];
                                    }
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        /// <summary>
        /// 配置options
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureOptions(ServiceConfigurationContext context)
        {
            context.Services.Configure<JwtOptions>(context.Services.GetConfiguration().GetSection("Jwt"));
        }

        /// <summary>
        /// Redis缓存
        /// </summary>
        private void ConfigureCache(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "ProjectName:"; });
            var configuration = context.Services.GetConfiguration();
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "ProjectName-Protection-Keys");
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<ProjectNameHttpApiHostModule>(); });

            // var hostingEnvironment = context.Services.GetHostingEnvironment();
            //
            // if (hostingEnvironment.IsDevelopment())
            // {
            //     Configure<AbpVirtualFileSystemOptions>(options =>
            //     {
            //         options.FileSets.ReplaceEmbeddedByPhysical<ProjectNameDomainSharedModule>(
            //             Path.Combine(hostingEnvironment.ContentRootPath,
            //                 $"..{Path.DirectorySeparatorChar}CompanyName.ProjectName.Domain.Shared"));
            //         options.FileSets.ReplaceEmbeddedByPhysical<ProjectNameDomainModule>(
            //             Path.Combine(hostingEnvironment.ContentRootPath,
            //                 $"..{Path.DirectorySeparatorChar}CompanyName.ProjectName.Domain"));
            //         options.FileSets.ReplaceEmbeddedByPhysical<ProjectNameApplicationContractsModule>(
            //             Path.Combine(hostingEnvironment.ContentRootPath,
            //                 $"..{Path.DirectorySeparatorChar}CompanyName.ProjectName.Application.Contracts"));
            //         options.FileSets.ReplaceEmbeddedByPhysical<ProjectNameApplicationModule>(
            //             Path.Combine(hostingEnvironment.ContentRootPath,
            //                 $"..{Path.DirectorySeparatorChar}CompanyName.ProjectName.Application"));
            //     });
            // }
        }

        private void ConfigurationStsHttpClient(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient(HttpClientNameConsts.Sts,
                options =>
                {
                    options.BaseAddress =
                        new Uri(context.Services.GetConfiguration().GetSection("HttpClient:Sts:Url").Value);
                });
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ProjectNameApplicationModule).Assembly);
            });
        }


        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureHealthChecks(ServiceConfigurationContext context)
        {
            var redisConnectionString =
                context.Services.GetConfiguration().GetValue<string>("Cache:Redis:ConnectionString");
            var redisDatabaseId = context.Services.GetConfiguration().GetValue<int>("Cache:Redis:DatabaseId");
            var password = context.Services.GetConfiguration().GetValue<string>("Cache:Redis:Password");
            var connectString = $"{redisConnectionString},password={password},defaultdatabase={redisDatabaseId}";
            context.Services.AddHealthChecks().AddRedis(redisConnectionString).AddMySql(connectString);
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "CompanyNameProjectName API", Version = "v1"});
                    options.DocInclusionPredicate((docName, description) => true);
                    options.EnableAnnotations(); // 启用注解
                    options.DocumentFilter<HiddenAbpDefaultApiFilter>();
                    options.SchemaFilter<EnumSchemaFilter>();
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
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    });

                    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Name = "Accept-Language",
                        Description = "多语言"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "ApiKey"}
                            },
                            new string[] { }
                        }
                    });
                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
                options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
                options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
                options.Languages.Add(new LanguageInfo("es", "es", "Español", "es"));
            });
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);
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

        private void ConfigurationCap(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var enabled = configuration.GetValue<bool>("Cap:Enabled", false);
            if (enabled)
            {
                context.AddAbpCap(capOptions =>
                {
                    capOptions.UseEntityFramework<ProjectNameDbContext>();
                    capOptions.UseRabbitMQ(option =>
                    {
                        option.HostName = configuration.GetValue<string>("Cap:RabbitMq:HostName");
                        option.UserName = configuration.GetValue<string>("Cap:RabbitMq:UserName");
                        option.Password = configuration.GetValue<string>("Cap:RabbitMq:Password");
                    });

                    var hostingEnvironment = context.Services.GetHostingEnvironment();
                    bool auth = !hostingEnvironment.IsDevelopment();
                    capOptions.UseDashboard(options => { options.UseAuth = auth; });
                });
            }
            else
            {
                context.AddAbpCap(capOptions =>
                {
                    capOptions.UseInMemoryStorage();
                    capOptions.UseInMemoryMessageQueue();
                    var hostingEnvironment = context.Services.GetHostingEnvironment();
                    bool auth = !hostingEnvironment.IsDevelopment();
                    capOptions.UseDashboard(options => { options.UseAuth = auth; });
                });
            }
        }
    }
}