---
outline: deep
---

# 统一返回值格式

## 使用

- 在 Controller 上或者 Action 上打上 WrapResultAttribute 特性
```csharp
[Route("Permissions")]
[WrapResult]
public class PermissionController : AbpProController,IRolePermissionAppService
{
    private readonly IRolePermissionAppService _rolePermissionAppService;
    public PermissionController(IRolePermissionAppService rolePermissionAppService)
    {
        _rolePermissionAppService = rolePermissionAppService;
    }
    [HttpPost("tree")]
    [SwaggerOperation(summary: "获取角色权限", Tags = new[] { "Permissions" })]
    [WrapResult] //控制器上打了 action上就不需要
    public Task<PermissionOutput> GetPermissionAsync(GetPermissionInput input)
    {
        return _rolePermissionAppService.GetPermissionAsync(input);
    }
}
```
- 格式如下:

```csharp
{
    // 返回格式类似这种
    "success": false,
    "message": "请求失败",
    "data": null,
    "code": 500
}
```

- 在使用Abp的过程中，如果提供给第三方接口要实现返回值统一的时候，可以使用这个特性标签。

## 实现原理

- 定义返回类型

```csharp
public class WrapResult<T>
{
        public bool Success { get; private set; }

        public string Message { get; private set; }

        public T Data { get; private set; }

        public int Code { get; private set; }

        public WrapResult()
        {
            Success = true;
            Message = "Success";
            Data = default;
            Code = 200;
        }

        public void SetSuccess(T data, string message = "Success", int code = 200)
        {
            Success = true;
            Data = data;
            Code = code;
        }

        public void SetFail(string message = "Fail", int code = 500)
        {
            Success = false;
            Message = message;
            Code = code;
        }
}
```

- 定义 WrapResultAttribute

```csharp
public class WrapResultAttribute : Attribute
{
}
```

- 添加异常过滤器(拦截异常,抛异常时指定返回格式)

  - [LionExceptionFilter](https://github.com/WangJunZzz/abp-vnext-pro/blob/main/aspnet-core/shared/Lion.AbpPro.Shared.Hosting.Microservices/Microsoft/AspNetCore/Mvc/Filters/LionExceptionFilter.cs)

- 添加结果过滤器
  - [LionResultFilter](https://github.com/WangJunZzz/abp-vnext-pro/blob/main/aspnet-core/shared/Lion.AbpPro.Shared.Hosting.Microservices/Microsoft/AspNetCore/Mvc/Filters/LionResultFilter.cs)

## 注册 Filter

```csharp
    /// <summary>
    /// 异常处理
    /// </summary>
    private void ConfigureAbpExceptions(ServiceConfigurationContext context)
    {
        context.Services.AddMvc
        (
            options =>
            {
                options.Filters.Add(typeof(LionExceptionFilter));
                options.Filters.Add(typeof(LionResultFilter));
            }
        );
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureAbpExceptions(context);
    }
```


