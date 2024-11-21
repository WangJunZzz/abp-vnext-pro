---
outline: deep
---

# 跨域(CORS)

- 允许指定策略

```json [appsetting.json]
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
