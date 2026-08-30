# ABP 10.2.0 升级到 10.6.0 指南

本文记录本项目从 ABP 10.2.0 升级到 10.6.0 的实际步骤和注意事项。升级范围以项目实际引用的模块为准，未使用的 ABP 模块不需要额外安装或升级。

## 1. 准备工作

```powershell
git switch -c codex/abp-10.6-nuget-upgrade
git status
```

确认工作区已有改动后再开始升级。`vben28` 目录不参与升级，也不要批量格式化或替换其中的文件。

建议先备份数据库和当前 NuGet 配置，并记录升级前的还原、编译结果。

## 2. SDK 和 NuGet

本项目目标框架为 `net10.0`。如果 `global.json` 锁定了本机不存在的 SDK（例如 `10.0.0`），应删除它，或改成团队统一安装且确实存在的 SDK 版本。删除后使用本机可用的 .NET 10 SDK。

集中版本文件需要同时检查主项目和模板：

- `aspnet-core/Directory.Build.Volo.targets`
- `aspnet-core/Directory.Build.Microsoft.targets`
- `templates/pro-module/Directory.Build.Volo.targets`
- `templates/pro-module/Directory.Build.Microsoft.targets`
- `templates/pro-nuget/service/Directory.Build.Volo.targets`
- `templates/pro-nuget/service/Directory.Build.Microsoft.targets`

将实际使用的 `Volo.Abp.*` 包升级到 `10.6.0`。实际使用的 Microsoft/EF Core `10.0.2` 包升级到当前目标补丁版本（本次为 `10.0.10`）。不要为了版本整齐而引入项目未使用的 ABP 模块。

升级后执行：

```powershell
dotnet restore aspnet-core/services/host/Lion.AbpPro.HttpApi.Host/Lion.AbpPro.HttpApi.Host.csproj
dotnet list aspnet-core/services/host/Lion.AbpPro.HttpApi.Host/Lion.AbpPro.HttpApi.Host.csproj package --outdated
dotnet list aspnet-core/services/host/Lion.AbpPro.HttpApi.Host/Lion.AbpPro.HttpApi.Host.csproj package --vulnerable
```

如果出现 `NU1900`，表示 NuGet 无法访问漏洞审计服务（常见原因是代理、防火墙或 TLS），不是漏洞扫描通过，也不是项目编译失败。应先恢复到 `https://api.nuget.org/v3/index.json` 的网络连接，不建议直接关闭审计。

## 3. SQLite 测试依赖

如果测试项目通过 ABP SQLite provider 运行内存数据库，应显式引用：

```xml
<PackageReference Include="SQLitePCLRaw.lib.e_sqlite3" />
```

集中版本设为 `2.1.12`，用于替代存在安全告警的 `2.1.11`。只在实际使用 SQLite 的测试项目中添加该引用。

## 4. CAP 事件总线适配

ABP 10.6 为 `EventBusBase` 和 `IDistributedEventBus` 增加了字符串事件名 API。自定义 CAP 适配类必须补齐：

- `Subscribe(string, IEventHandlerFactory)`
- `Unsubscribe(string, IEventHandlerFactory)`
- `Unsubscribe(string, IEventHandler)`
- `UnsubscribeAll(string)`
- `PublishAsync(string, object, bool)`
- `PublishAsync(string, object, bool, bool)`
- `GetDynamicHandlerFactories(string)`
- `GetEventTypeByEventName(string)`
- `Subscribe(string, IDistributedEventHandler<DynamicEventData>)`

本项目的实现位于 `frameworks/src/Lion.AbpPro.CAP/Lion/AbpPro/CAP/AbpProCapDistributedEventBus.cs`。动态事件发布时必须保留调用方传入的事件名；发送到 CAP 的内容使用原始 payload，同时继续保留租户 Header、Unit of Work 和 outbox 行为。

## 5. EF Core Migrations

本次升级不处理既有 Migrations 内容。可以在升级分支中删除旧迁移，或由维护人员先手动删除；升级完成后根据实际模型重新生成迁移：

```powershell
dotnet ef migrations add Abp106Upgrade \
  --project aspnet-core/services/src/Lion.AbpPro.EntityFrameworkCore \
  --startup-project aspnet-core/services/host/Lion.AbpPro.HttpApi.Host
```

生成迁移前先确认数据库备份和连接配置，避免在生产库直接执行升级迁移。

## 6. 验证顺序

建议串行执行，避免多个项目同时写入共享框架输出目录：

```powershell
dotnet build aspnet-core/frameworks/src/Lion.AbpPro.CAP/Lion.AbpPro.CAP.csproj --no-restore
dotnet build aspnet-core/services/host/Lion.AbpPro.HttpApi.Host/Lion.AbpPro.HttpApi.Host.csproj --no-restore
dotnet build aspnet-core/gateways/Lion.AbpPro.WebGateway/Lion.AbpPro.WebGateway.csproj --no-restore
dotnet build aspnet-core/modules/FileManagement/src/Lion.AbpPro.FileManagement.Application/Lion.AbpPro.FileManagement.Application.csproj --no-restore
dotnet build aspnet-core/modules/LanguageManagement/src/Lion.AbpPro.LanguageManagement.Application/Lion.AbpPro.LanguageManagement.Application.csproj --no-restore
```

然后运行实际存在的测试项目。若测试程序集提示“没有可用测试”，只能说明测试未被发现，不能当作测试通过；应检查测试 SDK、测试适配器和测试项目内容。

## 7. 提交前检查

```powershell
git diff --check
git status --short
```

确认以下项目没有被意外修改：

- `vben28/**`
- 与本次升级无关的业务代码
- 不需要升级的未使用 ABP 模块

升级验证完成后再决定是否提交、合并或推送分支。本次升级过程不自动提交或推送代码。
