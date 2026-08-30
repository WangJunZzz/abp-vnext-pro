---
name: abp-pro-infrastructure
description: 本项目的基础设施集成规则。修改 CAP 分布式事件、Hangfire、Redis、SignalR、后台任务、租户传播、事务 Outbox 或 Host 集成时使用。
---

# ABP Pro 基础设施

## CAP 分布式事件

- 使用 `Lion.AbpPro.CAP`，不要在局部任务中替换消息总线。
- 发布消息时保留 `AbpProCapConsts.Tenant` 租户 Header。
- 保留 Unit of Work、直接发布、延迟发布和 CAP 事务 Outbox 的区别。
- 动态事件必须保留调用方传入的事件名，并向 CAP 发送原始 payload，不要把 `DynamicEventData` 包装对象直接作为业务消息。
- 修改 `AbpProCapDistributedEventBus` 时，同时兼容当前 ABP 版本的 CLR 类型事件和字符串事件 API。

## 后台任务

- 使用所属模块已有的 ABP Background Jobs 或 Hangfire 集成。
- 可能重试的任务应保持幂等。
- 通过既有事件或任务机制显式传递租户上下文。

## Redis、SignalR 和 Host

- 共享基础设施在合适的 Host 或框架模块中配置，遵循现有 Options 和模块依赖。
- 不把业务行为放入基础设施适配器。
- 未经明确要求，不新增 OpenIddict、IdentityServer 或其他身份认证栈。

## 验证

共享基础设施修改后按顺序执行：

```powershell
dotnet build aspnet-core/frameworks/src/Lion.AbpPro.CAP/Lion.AbpPro.CAP.csproj --no-restore
dotnet build aspnet-core/services/host/Lion.AbpPro.HttpApi.Host/Lion.AbpPro.HttpApi.Host.csproj --no-restore
dotnet build aspnet-core/gateways/Lion.AbpPro.WebGateway/Lion.AbpPro.WebGateway.csproj --no-restore
```
