# NuGet 升级后审计

## 当前工作区

- 分支：`codex/abp-10.6-nuget-upgrade`
- 未提交、未推送。
- `vben28` 无变更。
- EF `Migrations/**` 保持用户原有删除状态，本次未生成或恢复 migration。
- 删除 `aspnet-core/global.json`，解决无效 SDK pin `10.0.0`。

## 已实施的版本调整

- 实际解析的 `Volo.Abp.*` 直接包更新到 `10.6.0`。
- 实际使用的 Microsoft.Extensions/ASP.NET Core/EF Core 10.0.2 包更新到 10.0.10 补丁线。
- `Swashbuckle.AspNetCore.Annotations` 更新到 `10.2.3`，其传递 `Microsoft.OpenApi` 已解析到 `2.7.5`。
- 对使用 SQLite provider 的测试项目加入直接 `SQLitePCLRaw.lib.e_sqlite3` 引用，并集中指定 `2.1.12`，消除原 `2.1.11` 高危告警。

## Restore 结果

- FileManagement solution：成功，无 SQLite/NuGet 漏洞告警。
- LanguageManagement solution：成功，无 SQLite/NuGet 漏洞告警。
- BasicManagement solution：仍不能 restore，原因是 solution 引用了不存在的 `Lion.AbpPro.BasicManagement.HttpApi.Client.ConsoleTestApp.csproj`；本次未修改 `.sln`。

## Build/Test 结果

- FileManagement：串行 build 成功，0 错误；存在已有 CA2022 分析警告。
- LanguageManagement：build 成功，0 警告，0 错误。
- 两个 solution 的 `dotnet test --no-build` 均正常启动，但所有测试程序集均报告“没有可用测试”；当前无法据此宣称业务测试通过。未升级测试 SDK、xUnit 或测试适配器。

## 待确认事项

1. BasicManagement 缺失项目是否由你另行恢复，或允许在升级分支从 solution 移除该引用。
2. 测试项目没有可发现测试是否属于现有测试框架配置问题，是否另开任务处理。
3. 是否继续审计并升级 `Newtonsoft.Json 9.0.1` 传递依赖；这需要定位引入它的具体包，不能直接全局覆盖。

## 新发现的兼容阻断

服务主机/网关项目在编译 `Lion.AbpPro.CAP` 时失败。`AbpProCapDistributedEventBus` 继承 ABP 10.6.0 的 `EventBusBase` 后，未实现新的抽象成员，包括 `PublishAsync`、`Subscribe`、`Unsubscribe`、`UnsubscribeAll`、`GetDynamicHandlerFactories` 和 `GetEventTypeByEventName` 等。

这是自定义 CAP 适配层与 ABP 10.6 EventBus API 的代码级兼容问题，不能通过继续机械替换 NuGet 版本解决。当前不修改 `AbpProCapDistributedEventBus.cs`，等待确认是否进入 CAP 适配阶段。
