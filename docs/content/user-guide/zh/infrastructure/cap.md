# dotnetcore.cap

分布式事件总线系统允许发布和订阅跨应用/服务边界传输的事件. 你可以使用分布式事件总线在微服务或应用程序之间异步发送和接收消息.

## 安装

- 添加以下 NuGet 包到你的项目
  - Lion.AbpPro.CAP
  - Lion.AbpPro.CAP.EntityFrameworkCore
  - DotNetCore.CAP.MySql (如果是其它数据库,请安装对应类型)
  - DotNetCore.CAP.RabbitMQ (如果是其它中间件,请安装对应类型)
- 添加 [DependsOn(typeof(LionAbpProCapEntityFrameworkCoreModule))] 到你的项目模块类.

## 配置

Mysql,和 RabbitMq 为例

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
        context.AddAbpCap(capOptions =>
        {
            // 指定数据数据库连接字符串
            capOptions.SetCapDbConnectionString(configuration["ConnectionStrings:Default"]);
            capOptions.UseEntityFramework<AbpProDbContext>();
            // 使用rabbitmq,配置host,username,password
            capOptions.UseRabbitMQ(option =>
            {
                option.HostName = configuration.GetValue<string>("Cap:RabbitMq:HostName");
                option.UserName = configuration.GetValue<string>("Cap:RabbitMq:UserName");
                option.Password = configuration.GetValue<string>("Cap:RabbitMq:Password");
            });

            var hostingEnvironment = context.Services.GetHostingEnvironment();
            bool auth = !hostingEnvironment.IsDevelopment();
            // 启用面板
            capOptions.UseDashboard(options =>
            {
                options.UseAuth = auth;
                options.AuthorizationPolicy = LionAbpProCapPermissions.CapManagement.Cap;
            });
        });
}
```

- 即可使用 Abp vNext 标准写法,发送分布式事件和订阅事件
