---
outline: deep
---

# Serilog

> ABP 框架没有实现任何日志基础设施. 它使用 ASP.NET Core 日志系统.

## 日志等级
::: danger 注意
 "Debug --> Information --> Warning --> Error --> Fatal"
:::

## 如何集成

```cs [Program.cs]
public class Program
{
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults()
            // 使用Serilog
            .UseSerilog((context, loggerConfiguration) =>
            {
                SerilogToEsExtensions.SetSerilogConfiguration(
                    loggerConfiguration,
                    context.Configuration);
            }).UseAutofac();
}
```

```cs [SerilogToEsExtensions.cs]
public static void SetSerilogConfiguration(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
{
    // 默认读取 configuration 中 "Serilog" 节点下的配置
    loggerConfiguration
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext();
    // 如果要再日志加上自定义字段
    loggerConfiguration.Enrich.WithProperty("Application", applicationName);
}
```

## 配置

```json [appsetting.json]
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

## 写入 ElasticSearch

> AbpPro 已经集成 ElasticSearch 只需要通过配置文件启用即可。

- Enabled:是否启用
- Url:es 地址
- IndexFormat:es 索引
- UserName:用户名
- Password:密码

```json [appsetting.json]
"ElasticSearch": {
  "Enabled": "false",
  "Url": "http://es.cn",
  // 索引名必须小写
  "IndexFormat": "lion.abppro.development.{0:yyyy.MM.dd}",
  "UserName": "elastic",
  "Password": "aVVhjQ95RP7nbwNy"
},
```

## 使用

```cs
public class SampleAppService : AbpProAppService,ISampleAppService
{
    private readonly ILogger<SampleAppService> _logger;

    public SampleAppService(ILogger<SampleAppService> logger)
    {
        _logger = logger;
    }

    public async Task TestAsync()
    {
        _logger.LogDebug("LogDebug");
        _logger.LogInformation("LogInformation");
        _logger.LogWarning("LogWarning");
        _logger.LogError("LogError");
        _logger.LogTrace("LogTrace");
        await Task.CompletedTask;
    }
}
```

## 日志追加字段

```csharp
/// <summary>
/// 自定义追加日志内容
/// </summary>
/// <param name="logger"></param>
/// <param name="message">日志内容</param>
/// <param name="level">日志级别</param>
/// <param name="dictionary">追加内容</param>
public static void Custom(this ILogger logger, string message, LogLevel level = LogLevel.Information, Dictionary<string, object> dictionary = null)
{
    if (dictionary == null || dictionary.Count == 0)
    {
        logger.Log(level, message);
    }
    else
    {
        using (logger.BeginScope(dictionary))
        {
            logger.Log(level, message);
        }
    }
}
```
