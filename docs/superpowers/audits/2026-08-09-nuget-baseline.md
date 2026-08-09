# NuGet 升级基线

## 执行环境

- 分支：`codex/abp-10.6-nuget-upgrade`
- SDK：删除无效的 `aspnet-core/global.json` 后，解析为 .NET SDK `10.0.101`
- 目标框架：主要项目为 `net10.0`
- 前端：`vben28` 冻结
- EF：现有 `Migrations/**` 不纳入本阶段审计

## 解决方案状态

- `modules/FileManagement/Lion.AbpPro.FileManagement.sln`：restore 成功，但有 NuGet 漏洞告警。
- `modules/LanguageManagement/Lion.AbpPro.LanguageManagement.sln`：restore 成功，但有 NuGet 漏洞告警。
- `modules/BasicManagement/Lion.AbpPro.BasicManagement.sln`：restore 被已有缺失项目阻断：
  `test/Lion.AbpPro.BasicManagement.HttpApi.Client.ConsoleTestApp/Lion.AbpPro.BasicManagement.HttpApi.Client.ConsoleTestApp.csproj`。
  本次不修改 `.sln`，后续对存在的项目单独审计。

## 已识别的 ABP 更新

`dotnet list package --outdated` 在 FileManagement 和 LanguageManagement 中识别到实际使用的 `Volo.Abp.*` 包均可更新到 `10.6.0`，包括：

- `Volo.Abp.Validation`
- `Volo.Abp.BlobStoring`
- `Volo.Abp.Ddd.Domain`
- `Volo.Abp.Authorization`
- `Volo.Abp.Ddd.Application.Contracts`
- `Volo.Abp.Ddd.Application`
- `Volo.Abp.Caching`
- `Volo.Abp.SettingManagement.Domain`
- `Volo.Abp.EntityFrameworkCore`
- `Volo.Abp.Http.Client`
- `Volo.Abp.AspNetCore.Mvc`
- `Volo.Abp.EntityFrameworkCore.Sqlite`
- `Volo.Abp.Authorization`、`Volo.Abp.Autofac`、`Volo.Abp.TestBase`
- `Volo.Abp.AspNetCore.Serilog`
- `Volo.Abp.EntityFrameworkCore.PostgreSql`

实际变更前仍需以所有项目的 resolved graph 去重，不能按中央 targets 的全部 `Update` 行机械替换。

## 已识别的非 ABP 更新

- `Microsoft.Extensions.FileProviders.Embedded`：`10.0.2` → `10.0.10`。
- `Microsoft.EntityFrameworkCore.Tools`：`10.0.2` → `10.0.10`。
- `Microsoft.EntityFrameworkCore.Proxies`：`10.0.2` → `10.0.10`。
- `Swashbuckle.AspNetCore.Annotations`：`10.0.1` → `10.2.3`。
- `Microsoft.NET.Test.Sdk`：`17.14.1` → `18.8.1`，需先确认测试框架兼容性。
- `NSubstitute`：`5.3.0` → `6.1.0`，建议与测试回归分开评估。
- `xunit.runner.visualstudio`：`3.1.1` → `3.1.5`。
- `Microsoft.AspNetCore.Http.Abstractions`、`Microsoft.AspNetCore.Mvc.Core`、`Microsoft.AspNetCore.Http.Features` 当前为旧兼容包，不能仅因存在更新就直接升级，需检查实际 API 依赖。

## 安全告警

- `Microsoft.AspNetCore.DataProtection 10.0.2`：严重漏洞 `GHSA-9mv3-2cwr-p262`。
- `Microsoft.OpenApi 2.3.0`：高危漏洞 `GHSA-v5pm-xwqc-g5wc`。
- `System.Security.Cryptography.Xml 10.0.2`：多条高危告警。
- `Newtonsoft.Json 9.0.1`：高危漏洞 `GHSA-5crp-9r3c-p9vr`，在 FileManagement 应用相关项目中为传递依赖。
- `SQLitePCLRaw.lib.e_sqlite3 2.1.11`：高危漏洞 `GHSA-2m69-gcr7-jv3q`，主要出现在 SQLite 测试链路。

安全告警需在版本变更阶段一并处理或明确记录为暂缓项；不能以“不是 ABP 包”为理由忽略。

## 当前结论

1. 第一批必须处理：实际使用的 ABP 10.6.0 兼容升级，以及阻断性/严重安全告警的最小修复版本。
2. 第二批单独评估：测试 SDK、NSubstitute、xUnit runner、Swashbuckle 等非阻断更新。
3. 保持不动：未被解析的中央 targets `Update` 条目、`vben28`、EF `Migrations/**`。

