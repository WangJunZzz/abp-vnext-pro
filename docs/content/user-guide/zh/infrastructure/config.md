# 配置

## 日志

### 日志级别

!!! info "Debug --> Information --> Warning --> Error --> Fatal"

```json
"Serilog": {
  "Using": [
    "Serilog.Sinks.Console",
    "Serilog.Sinks.File"
  ],
  "MinimumLevel": {
    // 默认全局日志级别
    "Default": "Information", 
    "Override": { 
      //名称空间为 Microsoft 日志级别        
      "Microsoft": "Information", 
      //名称空间为 Volo.Abp 日志级别      
      "Volo.Abp": "Information",  
      //名称空间为 Hangfire 日志级别
      "Hangfire": "Information", 
      //名称空间为 DotNetCore.CAP 日志级别 
      "DotNetCore.CAP": "Information",  
      //名称空间为 Serilog.AspNetCore 日志级别
      "Serilog.AspNetCore": "Information", 
      //名称空间为 Microsoft.EntityFrameworkCore 日志级别
      "Microsoft.EntityFrameworkCore": "Warning", 
      //名称空间为 Microsoft.AspNetCore 日志级别
      "Microsoft.AspNetCore": "Information" 
    }
  },
  "WriteTo": [
    {
      // 输出到控制台日志
      "Name": "Console"  
    },
    {
      // 输出到文件
      "Name": "File", 
      "Args": {
        "path": "logs/logs-.txt",
         // 按天输出
        "rollingInterval": "Day" 
      }
    }
  ]
}
```

### 写入ES
!!! WARNING "先决条件：搭建好ES环境"
- Enabled:是否启用
- Url:es地址
- IndexFormat:es索引
- UserName:用户名
- Password:密码
- SearchIndexFormat:es日志查询索引模式

```json
"ElasticSearch": {
  "Enabled": "false", 
  "Url": "http://es.cn", 
  "IndexFormat": "Lion.AbpPro.development.{0:yyyy.MM.dd}", 
  "UserName": "elastic", 
  "Password": "aVVhjQ95RP7nbwNy",
  "SearchIndexFormat": "Lion.AbpPro.development*" 
},
```

- 查看Lion.AbpPro.HttpApi.Host.Program.cs

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
      
    }
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel((context, options) => { options.Limits.MaxRequestBodySize = 1024 * 50; });
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog((context, loggerConfiguration) =>
            {
                // 配置ES日志
                SerilogToEsExtensions.SetSerilogConfiguration(
                    loggerConfiguration,
                    context.Configuration);
            }).UseAutofac();
}
```


## 跨域(CORS)

- 允许指定策略
```json
"App": {
    // 逗号分隔
    "CorsOrigins": "http://*.com,http://localhost:4200"
  },
```
- 配置跨域

```csharp
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
```

## AccessToken

- Audience:接收对象
- Issuer:签发主体
- SecurityKey:密钥
- ExpirationTime:过期时间(单位小时)
```json
  "Jwt": {
    "Audience": "Lion.AbpPro", 
    "SecurityKey": "dzehzRz9a8asdfasfdadfasdfasdfafsdadfasbasdf=",
    "Issuer": "Lion.AbpPro", 
    "ExpirationTime": 30
  }
```

## CAP
!!! WARNING "如果要切换其他中间件请参考 [dotnetcore.cap](https://cap.dotnetcore.xyz/)"

- Enabled: 是否启用
- RabbitMq：Mq配置
```json
"Cap": {
  "Enabled": "true",
  "RabbitMq": {
    "HostName": "localhost",
    "UserName": "admin",
    "Password": "admin"
  }
}
```
