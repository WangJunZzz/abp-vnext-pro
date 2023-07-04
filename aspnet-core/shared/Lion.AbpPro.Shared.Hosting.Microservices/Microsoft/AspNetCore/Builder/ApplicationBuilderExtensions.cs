using Microsoft.AspNetCore.RequestLog;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static string UseConsul(this IApplicationBuilder app)
        {
            var appLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            using var scope = app.ApplicationServices.CreateScope();
            var configuration = scope.ServiceProvider.GetService<IConfiguration>();

            bool isEnabled = configuration.GetValue<bool>("Consul:Enabled");
            string serviceName = configuration.GetValue<string>("Consul:Service");
            var appString = configuration.GetValue<string>("App:SelfUrl");
            Uri appUrl = new Uri(appString, UriKind.Absolute);

            if (!isEnabled)
                return String.Empty;

            Guid serviceId = Guid.NewGuid();
            string consulServiceId = $"{serviceName}:{serviceId}";

            var client = scope.ServiceProvider.GetService<IConsulClient>();

            var consulServiceRegistration = new AgentServiceRegistration
            {
                Name = serviceName,
                ID = consulServiceId,
                Address = appUrl.Host,
                Port = appUrl.Port,
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), //服务停止多久后注销
                    Interval = TimeSpan.FromSeconds(3), //健康检查时间间隔，或者称为心跳 间隔
                    HTTP = $"http://{appUrl.Host}:{appUrl.Port}/health", //健康检查地址 
                    Timeout = TimeSpan.FromSeconds(15) //超时时间
                }
            };

            client.Agent.ServiceRegister(consulServiceRegistration);
            appLifetime.ApplicationStopping.Register(() => { client.Agent.ServiceDeregister(consulServiceRegistration.ID); });

            return consulServiceId;
        }

        /// <summary>
        /// 记录请求响应日志
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLogMiddleware>();
        }
        
        /// <summary>
        /// 多语言中间件
        /// <remarks>浏览器传递的请求头：Accept-Language: zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6而abp钟简体中文为：zh-Hans</remarks>
        /// <example>
        /// app.UseAbpProRequestLocalization();
        /// </example>
        /// </summary>
        public static IApplicationBuilder UseAbpProRequestLocalization(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseAbpRequestLocalization(options =>
            {
                // 移除自带header解析器
                options.RequestCultureProviders.RemoveAll(provider=> provider is AcceptLanguageHeaderRequestCultureProvider);
                // 添加header解析器
                options.RequestCultureProviders.Add(new AbpProAcceptLanguageHeaderRequestCultureProvider());
            });
        }
    }
}