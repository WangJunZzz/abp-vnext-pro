using Volo.Abp.BackgroundJobs.Hangfire;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProHttpApiModule),
        typeof(SharedHostingMicroserviceModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpProEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAccountWebModule),
        typeof(AbpProApplicationModule),
        typeof(AbpProAbpCapModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpBackgroundJobsHangfireModule)
    )]
    public class AbpProHttpApiHostModule : AbpModule
    {
        public override void OnPostApplicationInitialization(
            ApplicationInitializationContext context)
        {
            // 应用程序初始化的时候注册hangfire
            context.CreateRecurringJob();
            base.OnPostApplicationInitialization(context);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            ConfigureCache(context);
            ConfigureSwaggerServices(context);
            ConfigureJwtAuthentication(context, configuration);
            ConfigureHangfireMysql(context);
            ConfigureHttpClient(context);
            ConfigureMiniProfiler(context);
            ConfigureAbpExceptions(context);
            ConfigureIdentity(context);
            ConfigureCap(context);
            ConfigureAuditLog(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();
            //app.UseRequestLog();
            app.UseAbpRequestLocalization();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseMiniProfiler();
            app.UseRouting();
            app.UseCors(AbpProHttpApiHostConst.DefaultCorsPolicyName);
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/AbpPro/swagger.json", "AbpPro API");
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();

            app.UseUnitOfWork();
            app.UseConfiguredEndpoints(endpoints => { endpoints.MapHealthChecks("/health"); });
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new CustomHangfireAuthorizeFilter() },
                IgnoreAntiforgeryToken = true
            });

            if (configuration.GetValue("Consul:Enabled", false))
            {
                app.UseConsul();
            }
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureAbpExceptions(ServiceConfigurationContext context)
        {
            context.Services.AddMvc(options => { options.Filters.Add(typeof(ResultExceptionFilter)); });
        }

   

        private void ConfigureHangfireMysql(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => { options.IsJobExecutionEnabled = true; });
            context.Services.AddHangfireServer();
            context.Services.AddHangfire(config =>
            {
                config.UseStorage(new MySqlStorage(
                    context.Services.GetConfiguration().GetConnectionString("Default"),
                    new MySqlStorageOptions()
                    {
                        //CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        //SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        //QueuePollInterval = TimeSpan.Zero,
                        //UseRecommendedIsolationLevel = true,
                        //DisableGlobalLocks = true
                        JobExpirationCheckInterval = TimeSpan.FromMinutes(30),
                        TablesPrefix = "Hangfire_"
                    }));
                var delaysInSeconds = new[] { 10, 60, 60 * 3 }; // 重试时间间隔
                const int Attempts = 3; // 重试次数
                config.UseFilter(new AutomaticRetryAttribute() { Attempts = Attempts, DelaysInSeconds = delaysInSeconds });
                config.UseFilter(new AutoDeleteAfterSuccessAttributer(TimeSpan.FromDays(7)));
                config.UseFilter(new JobRetryLastFilter(Attempts));
            });
        }

        /// <summary>
        /// 配置MiniProfiler
        /// </summary>
        private void ConfigureMiniProfiler(ServiceConfigurationContext context)
        {
            context.Services.AddMiniProfiler(options => options.RouteBasePath = "/profiler").AddEntityFramework();
        }

        /// <summary>
        /// 配置JWT
        /// </summary>
        private void ConfigureJwtAuthentication(ServiceConfigurationContext context,
            IConfiguration configuration)
        {
            context.Services.AddAuthentication(options =>
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
                            //ClockSkew = TimeSpan.Zero,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidAudience = configuration["Jwt:Audience"],
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.ASCII.GetBytes(configuration["Jwt:SecurityKey"]))
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
                                accessToken = currentContext.Request.Cookies[AbpProHttpApiHostConst.DefaultCookieName];
                            }

                            currentContext.Token = accessToken;
                            currentContext.Request.Headers.Remove("Authorization");
                            currentContext.Request.Headers.Add("Authorization", $"Bearer {accessToken}");

                            return Task.CompletedTask;
                        }
                    };
                });
        }

   

        /// <summary>
        /// Redis缓存
        /// </summary>
        private void ConfigureCache(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedCacheOptions>(
                options => { options.KeyPrefix = "AbpPro:"; });
            var configuration = context.Services.GetConfiguration();
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "AbpPro-Protection-Keys");
        }


        private void ConfigureHttpClient(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient(context.Services.GetConfiguration().GetSection("HttpClient:Sts:Name").Value,
                options =>
                {
                    options.BaseAddress =
                        new Uri(context.Services.GetConfiguration().GetSection("HttpClient:Sts:Url")
                            .Value);
                });
            context.Services.AddHttpClient(context.Services.GetConfiguration().GetSection("HttpClient:Github:Name").Value,
                options =>
                {
                    options.BaseAddress =
                        new Uri(context.Services.GetConfiguration().GetSection("HttpClient:Github:Url")
                            .Value);
                });
            context.Services.AddHttpClient(context.Services.GetConfiguration().GetSection("HttpClient:GithubApi:Name").Value,
                options =>
                {
                    options.BaseAddress =
                        new Uri(context.Services.GetConfiguration().GetSection("HttpClient:GithubApi:Url")
                            .Value);
                });
          
        }

        /// <summary>
        /// 配置Identity
        /// </summary>
        private void ConfigureIdentity(ServiceConfigurationContext context)
        {
            context.Services.Configure<IdentityOptions>(options => { options.Lockout = new LockoutOptions() { AllowedForNewUsers = false }; });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    // 文件下载类型
                    options.MapType<FileContentResult>(() => new OpenApiSchema() { Type = "file" });

                    options.SwaggerDoc("AbpPro",
                        new OpenApiInfo { Title = "LionAbpPro API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.EnableAnnotations(); // 启用注解
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
        }


        private void ConfigureCap(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var enabled = configuration.GetValue("Cap:Enabled", false);
            if (enabled)
            {
                context.AddAbpCap(capOptions =>
                {
                    capOptions.UseEntityFramework<AbpProDbContext>();
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
        
        /// <summary>
        /// 审计日志
        /// </summary>
        private void ConfigureAuditLog(ServiceConfigurationContext context)
        {
            Configure<AbpAuditingOptions>
            (
                options =>
                {
                    options.IsEnabled = true;
                    options.EntityHistorySelectors.AddAllEntities();
                    options.ApplicationName = "Lion.AbpPro";
                }
            );

            Configure<AbpAspNetCoreAuditingOptions>(
                options =>
                {
                    options.IgnoredUrls.Add("/AuditLogs/page");
                    options.IgnoredUrls.Add("/hangfire/stats");
                    options.IgnoredUrls.Add("/cap");
                });
        }
    }
}