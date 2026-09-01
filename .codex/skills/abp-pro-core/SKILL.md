---
name: abp-pro-core
description: 本项目的 ABP Pro 核心约定。修改 aspnet-core 后端 C#、ABP 模块、依赖注入、异常、本地化、权限、多租户或时间处理时使用。
---

# ABP Pro 核心约定

## 项目基线

- 目标框架为 `net10.0`，ABP 版本为 `10.6.0`。
- 除非用户明确要求，后端任务不得修改 `vben28/**`。
- 遵循 `aspnet-core/services`、`aspnet-core/frameworks` 和 `aspnet-core/modules` 的现有分层结构。
- 项目使用 Mapster，不引入 AutoMapper。
- 未经明确要求，不创建、恢复或修改 EF Migrations。

## ABP 约定

- 通过 `AbpModule` 和 `[DependsOn]` 管理模块依赖。
- 可复用模块在 `ConfigureServices` 中配置；HTTP 管道只在最终 Host 配置。
- 优先使用 `ITransientDependency`、`IScopedDependency`、`ISingletonDependency` 和现有模块注册方式。
- 使用 `ApplicationService`、`DomainService`、`AbpController` 已提供的 `Clock`、`GuidGenerator`、`CurrentUser`、`CurrentTenant`、`L`、`AuthorizationService`、`DataFilter` 等属性。
- 使用 `Clock.Now`，不要直接使用 `DateTime.Now` 或 `DateTime.UtcNow`。
- 异步调用保持到底，不使用 `.Result` 或 `.Wait()`。

## 异常、本地化和安全

- 业务规则失败使用带模块命名空间的 `BusinessException`。
- 用户可见文本和异常码翻译放在所属模块的 `Domain.Shared/Localization`。
- 使用 ABP 权限和 `[Authorize]`、`CheckPolicyAsync`，不要在应用服务或控制器中硬编码角色。
- 尊重 `CurrentTenant` 和 ABP 数据过滤器；只在有明确理由的短作用域内禁用过滤器。

## 映射

- 遵循附近代码已有的 Mapster 注册和映射方式。
- 映射放在实体与 DTO 的层边界，不为单个转换引入第二套映射框架。

## 验证

共享框架修改后先构建最小受影响项目，再构建 Host：

```powershell
dotnet build aspnet-core/services/host/Lion.AbpPro.HttpApi.Host/Lion.AbpPro.HttpApi.Host.csproj --no-restore
```
