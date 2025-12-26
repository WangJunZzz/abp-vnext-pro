using Consul;
using Lion.AbpPro.AspNetCore;
using Lion.AbpPro.AspNetCore.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Swagger;
using Volo.Abp.AspNetCore.Auditing;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Auditing;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{


    /// <summary>
    /// Ocelot配置
    /// </summary>
    public static IServiceCollection AddAbpProOcelot(this IServiceCollection service)
    {
        var configuration = service.GetConfiguration();
        service.AddOcelot(configuration).AddConsul().AddPolly();
        return service;
    }

    public static IServiceCollection AddAbpProConsul(this IServiceCollection service)
    {
        var consulOptions = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProConsulOptions>>().Value;
        if (!consulOptions.Enabled)
            return service;

        service.AddSingleton<IConsulClient>(p => new ConsulClient(config => { config.Address = new Uri(consulOptions.ServiceUrl); }));

        return service;
    }

    /// <summary>
    /// 健康检查
    /// </summary>
    public static IServiceCollection AddAbpProHealthChecks(this IServiceCollection service)
    {
        // TODO 检查数据库和redis是否正常 AspNetCore.HealthChecks.Redis AspNetCore.HealthChecks.MySql
        // context.Services.AddHealthChecks().AddRedis(redisConnectionString).AddMySql(connectString);
        service.AddHealthChecks();
        return service;
    }

    /// <summary>
    /// 配置租户解析
    /// </summary>
    public static IServiceCollection AddAbpProTenantResolvers(this IServiceCollection service)
    {
        service.Configure<AbpTenantResolveOptions>(options =>
        {
            options.TenantResolvers.Clear();
            // 只保留通过请求头解析租户
            // options.TenantResolvers.Add(new QueryStringTenantResolveContributor());
            // options.TenantResolvers.Add(new RouteTenantResolveContributor());
            options.TenantResolvers.Add(new HeaderTenantResolveContributor());
            options.TenantResolvers.Add(new CookieTenantResolveContributor());
        });

        return service;
    }

    /// <summary>
    /// 多语言配置
    /// </summary>
    public static IServiceCollection AddAbpProLocalization(this IServiceCollection service)
    {
        service.Configure<AbpLocalizationOptions>(options =>
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
        });
        return service;
    }

    /// <summary>
    /// 配置跨域
    /// </summary>
    public static IServiceCollection AddAbpProCors(this IServiceCollection service)
    {
        var corsOptions = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProCorsOptions>>().Value;
        if (!corsOptions.Enabled) return service;
        
        service.AddCors(options =>
        {
            options.AddPolicy(AbpProAspNetCoreConsts.DefaultCorsPolicyName, builder =>
            {
                builder
                    .WithOrigins(
                        corsOptions.CorsOrigins
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    //.WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.AllowCredentials()
                    // https://www.cnblogs.com/JulianHuang/p/14225515.html
                    // https://learn.microsoft.com/zh-cn/aspnet/core/security/cors?view=aspnetcore-7.0
                    .SetPreflightMaxAge((TimeSpan.FromHours(24)));
            });
        });
        return service;
    }

    /// <summary>
    /// 阻止跨站点请求伪造
    /// https://docs.microsoft.com/zh-cn/aspnet/core/security/anti-request-forgery?view=aspnetcore-6.0
    /// </summary>
    public static IServiceCollection AddAbpProAntiForgery(this IServiceCollection service)
    {
        var antiForgeryOptions = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProAntiForgeryOptions>>().Value;
        if (antiForgeryOptions.Enabled)
        {
            service.Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = antiForgeryOptions.Enabled; });
        }
        
        return service;
    }

    /// <summary>
    /// 异常处理
    /// </summary>
    public static IServiceCollection AddAbpProExceptions(this IServiceCollection service)
    {
        service.AddMvc(options =>
            {
                options.Filters.Add(typeof(AbpProExceptionFilter));
                options.Filters.Add(typeof(AbpProResultFilter));
            }
        );
        return service;
    }

    /// <summary>
    /// 添加swagger
    /// </summary>
    public static IServiceCollection AddAbpProSwagger(this IServiceCollection service, string name, string version = "v1")
    {
        var swaggerOptions = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProSwaggerOptions>>().Value;
        if (!swaggerOptions.Enabled) return service;

        service.AddSwaggerGen(options =>
        {
            // 文件下载类型
            options.MapType<FileContentResult>(() => new OpenApiSchema() { Type = "file" });
            options.SwaggerDoc(name, new OpenApiInfo { Title = name, Version = version });
            options.DocInclusionPredicate((docName, description) => true);
            //options.EnableAnnotations(); // 启用注解
            options.DocumentFilter<HiddenAbpDefaultApiFilter>();
            options.SchemaFilter<EnumSchemaFilter>();
            // 加载所有xml注释，这里会导致swagger加载有点缓慢
            var xmlPaths = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            foreach (var xml in xmlPaths)
            {
                options.IncludeXmlComments(xml, true);
            }

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme()
                {
                    Description = "直接在下框输入JWT生成的Token",
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
                Description = "多语言设置，系统预设语言有zh-Hans、en，默认为zh-Hans",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                            { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return service;
    }

    /// <summary>
    /// 配置MiniProfiler
    /// </summary>
    public static IServiceCollection AddAbpProMiniProfiler(this IServiceCollection service)
    {
        var options = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProMiniProfilerOptions>>().Value;
        if (!options.Enabled) return service;
        service.AddMiniProfiler(opt => opt.RouteBasePath = options.RouteBasePath).AddEntityFramework();
        return service;
    }

    /// <summary>
    /// 添加多租户支持
    /// </summary>
    public static IServiceCollection AddAbpProMultiTenancy(this IServiceCollection service)
    {
        var multiTenancyOptions = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProMultiTenancyOptions>>().Value;
        service.Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = multiTenancyOptions.Enabled; });
        return service;
    }
    
     /// <summary>
    /// 配置JWT
    /// </summary>
    public static IServiceCollection AddAbpProAuthentication(this IServiceCollection service)
    {
        var jwtOptions = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProJwtOptions>>().Value;
        var cookieOptions = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProCookieOptions>>().Value;
        
        service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters()
                    {
                        // 是否开启签名认证
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(jwtOptions.SecurityKey))
                    };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = currentContext =>
                    {
                        var path = currentContext.HttpContext.Request.Path;
                        if (path.StartsWithSegments("/login"))
                        {
                            return Task.CompletedTask;
                        }

                        var accessToken = string.Empty;
                        if (currentContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
                        {
                            accessToken = currentContext.HttpContext.Request.Headers["Authorization"];
                            if (!string.IsNullOrWhiteSpace(accessToken))
                            {
                                accessToken = accessToken.Split(" ").LastOrDefault();
                            }
                        }

                        if (accessToken.IsNullOrWhiteSpace())
                        {
                            accessToken = currentContext.Request.Query["access_token"].FirstOrDefault();
                        }

                        if (accessToken.IsNullOrWhiteSpace())
                        {
                            accessToken = currentContext.Request.Cookies[cookieOptions.Name];
                        }

                        currentContext.Token = accessToken;
                        currentContext.Request.Headers.Remove("Authorization");
                        currentContext.Request.Headers.Append("Authorization", $"Bearer {accessToken}");

                        return Task.CompletedTask;
                    }
                };
            });

        return service;
    }
    
    
    /// <summary>
    /// 审计日志
    /// </summary>
    public static IServiceCollection AddAbpProAuditLog(this IServiceCollection service)
    {
        var auditOptions = service.BuildServiceProvider().GetRequiredService<IOptions<AbpProAuditOptions>>().Value;
        service.Configure<AbpAuditingOptions>
        (options =>
            {
                options.IsEnabled = auditOptions.Enabled;
                options.EntityHistorySelectors.AddAllEntities();
                options.ApplicationName = auditOptions.ApplicationName;
            }
        );

        service.Configure<AbpAspNetCoreAuditingOptions>(options =>
        {
            options.IgnoredUrls.Add("/AuditLogs/page");
            options.IgnoredUrls.Add("/hangfire/stats");
            options.IgnoredUrls.Add("/hangfire/recurring/trigger");
            options.IgnoredUrls.Add("/cap");
            options.IgnoredUrls.Add("/");
        });
        return service;
    }
}