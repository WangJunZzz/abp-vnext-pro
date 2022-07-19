namespace Lion.AbpPro.Shared.Hosting.Microservices.Microsoft.AspNetCore.Builder
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
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务停止多久后注销
                    Interval = TimeSpan.FromSeconds(3),//健康检查时间间隔，或者称为心跳 间隔
                    HTTP = $"http://{appUrl.Host}:{appUrl.Port}/health",//健康检查地址 
                    Timeout = TimeSpan.FromSeconds(15)   //超时时间
                }
            };

            client.Agent.ServiceRegister(consulServiceRegistration);
            appLifetime.ApplicationStopping.Register(() => { client.Agent.ServiceDeregister(consulServiceRegistration.ID); });

            return consulServiceId;
        }
    }
}