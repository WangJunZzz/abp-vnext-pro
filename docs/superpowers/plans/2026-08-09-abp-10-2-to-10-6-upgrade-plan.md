# ABP 10.2.0 到 10.6.0 NuGet 升级实施计划

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 在不修改 `vben28`、业务代码和 EF Migrations 的前提下，审计并升级后端及后端模板中实际使用的 NuGet 依赖，使其与 ABP 10.6.0 和 `net10.0` 兼容。

**Architecture:** 依赖版本继续由现有 `Directory.Build.*` 集中管理。先生成直接/传递依赖基线，再按“必须同步升级、建议升级、保持、排除”分类，只提交必须同步的版本变更；编译或运行时问题单独建立确认项。

**Tech Stack:** .NET 10、ABP 10.6.0、NuGet、EF Core、PostgreSQL/MySQL/MongoDB provider、Mapster、FreeSql、CAP、Hangfire、Nacos、Redis、SignalR、Serilog。

---

## 分支与提交策略

- 在当前工作区创建普通 Git 分支，不创建 worktree，不切换或修改 `vben28`。
- 升级过程中只保留工作区改动和审计结果，不自动 commit、不 push。
- 所有验证完成后先向项目负责人汇报 diff、依赖图和测试结果；得到确认后再统一提交。
- 升级前已有的 Filter 修改和 EF migration 删除保持原状，不纳入本次升级判断。

## 文件范围

允许修改：

- `aspnet-core/Directory.Build.Volo.targets`
- `aspnet-core/Directory.Build.Microsoft.targets`
- `aspnet-core/Directory.Build.targets`
- `templates/pro-module/Directory.Build.Volo.targets`
- `templates/pro-module/Directory.Build.Microsoft.targets`
- `templates/pro-module/Directory.Build.targets`
- `templates/pro-nuget/service/Directory.Build.Volo.targets`
- `templates/pro-nuget/service/Directory.Build.Microsoft.targets`
- `templates/pro-nuget/service/Directory.Build.targets`
- 实际项目显式写入版本的 `.csproj`、`.props`、`.targets` 或 NuGet 配置。
- Delete: `aspnet-core/global.json`（当前内容 `10.0.0` 无效，删除后使用已安装的 .NET 10 SDK）。

禁止修改：

- `vben28/**`
- 业务源码，除非 NuGet 10.6.0 造成已确认的编译/API 兼容错误。
- EF `Migrations/**`，本计划不要求保留、恢复或生成 migration。

## Task 1: 建立依赖和工作区基线

**Files:**

- Create: `docs/superpowers/audits/2026-08-09-nuget-baseline.md`
- Inspect: `aspnet-core/**/*.csproj`, `templates/**/*.csproj`, all `Directory.Build.*`

- [ ] **Step 1: Confirm existing user changes and frozen paths**

```powershell
git status --short
git diff --name-only -- vben28
```

Expected: 记录现有修改；`vben28` 不应出现在本次升级变更集中。

- [ ] **Step 2: Remove invalid SDK pin and capture framework baseline**

```powershell
dotnet --info
rg -n --hidden -g '*.csproj' 'TargetFramework|TargetFrameworks' aspnet-core templates
```

`aspnet-core/global.json` 当前声明的 `10.0.0` 不是合法 SDK feature-band；删除后记录实际解析的 SDK、OS 和目标框架。除删除这个无效 pin 外，不升级 SDK 或 TargetFramework。

- [ ] **Step 3: Enumerate direct package references**

```powershell
$projects = rg --files aspnet-core templates -g '*.csproj'
foreach ($project in $projects) {
    dotnet list $project package --format json
}
```

将每个项目的直接引用写入基线文档，标记生产、测试、模板和遗留用途。

- [ ] **Step 4: Record current restore/build/test result**

```powershell
dotnet restore aspnet-core/modules/BasicManagement/Lion.AbpPro.BasicManagement.sln
dotnet restore aspnet-core/modules/FileManagement/Lion.AbpPro.FileManagement.sln
dotnet restore aspnet-core/modules/LanguageManagement/Lion.AbpPro.LanguageManagement.sln
```

对三个解决方案分别执行 build/test，记录失败项目和失败原因，避免把升级前已有问题误判为回归。

- [ ] **Step 5: Keep the baseline audit uncommitted**

保留审计文件在当前工作区，等待全部升级验证结束后统一审阅；本阶段不执行 `git commit` 或 `git push`。

## Task 2: 完成全量 NuGet 审计

**Files:**

- Modify: `docs/superpowers/audits/2026-08-09-nuget-baseline.md`
- Inspect: `aspnet-core/Directory.Build.targets`, `aspnet-core/Directory.Build.Volo.targets`, `aspnet-core/Directory.Build.Microsoft.targets`, matching template files

- [ ] **Step 1: Capture direct and transitive dependency graphs**

```powershell
foreach ($project in $projects) {
    dotnet list $project package --include-transitive
    dotnet list $project package --outdated
    dotnet list $project package --vulnerable --include-transitive
}
```

- [ ] **Step 2: Classify every relevant package**

For each package record: source project, direct/transitive, current version, resolved version, target framework, usage, compatible target version, and one of `必须同步升级/建议升级/保持当前版本/排除`.

Required review groups: ABP/Volo, Microsoft/EF Core, Npgsql/Pomelo/MongoDB/SQLite, Mapster/FreeSql, CAP, Hangfire, Nacos, Redis, SignalR, Serilog/ElasticSearch, Swashbuckle, test SDK and test libraries.

- [ ] **Step 3: Validate special cases**

Confirm whether the single `Volo.Abp.AutoMapper` test reference is actually used. Keep it unchanged or remove it only as a separate cleanup decision. Do not introduce OpenIddict or AutoMapper into production modules.

- [ ] **Step 4: Keep the classification uncommitted**

将分类结果保留在当前工作区，等待项目负责人确认升级范围；本阶段不执行 `git commit` 或 `git push`。

## Task 3: Update only required package versions

**Files:**

- Modify only the version-bearing `Directory.Build.*`, `.csproj`, `.props`, `.targets` and NuGet files identified in Task 2.
- Do not modify `vben28/**` or `**/Migrations/**`.

- [ ] **Step 1: Update ABP package family**

Set only actually resolved `Volo.Abp.*` packages that are required by the audit to `10.6.0`. Do not add package references for unused modules.

- [ ] **Step 2: Align Microsoft and EF Core packages**

Set Microsoft.Extensions/ASP.NET Core/EF Core packages to the compatible patch line selected by the audit. Keep unrelated packages at their current versions unless classified as `必须同步升级`.

- [ ] **Step 3: Update required third-party packages**

Apply only the audit-approved changes for database providers, Mapster, FreeSql, CAP, Hangfire, Nacos, Redis, SignalR, Serilog, ElasticSearch, Swashbuckle and test packages. For SQLite test projects, use a direct `SQLitePCLRaw.lib.e_sqlite3` reference at `2.1.12` to override the vulnerable transitive `2.1.11`.

- [ ] **Step 4: Check for accidental scope expansion**

```powershell
git diff --name-only -- vben28
git diff --name-only -- '**/Migrations/**'
rg -n --hidden -g '!vben28/**' -g '!**/bin/**' -g '!**/obj/**' '10\.2\.0' aspnet-core templates
```

Expected: no `vben28` changes; no migration changes required; remaining 10.2.0 references are documented as excluded or unresolved transitive items.

- [ ] **Step 5: Review package-only diff without committing**

```powershell
git diff -- aspnet-core templates
git diff -- vben28
```

确认范围后继续验证；本阶段不执行 `git commit` 或 `git push`。

## Task 4: Restore and dependency-graph verification

**Files:**

- Create: `docs/superpowers/audits/2026-08-09-nuget-post-upgrade.md`

- [ ] **Step 1: Restore all backend and template projects**

```powershell
dotnet restore aspnet-core
dotnet restore templates/pro-module
dotnet restore templates/pro-nuget/service
```

- [ ] **Step 2: Verify resolved versions and downgrades**

```powershell
foreach ($project in $projects) {
    dotnet list $project package --include-transitive
}
```

Check that required ABP packages resolve to 10.6.0, no warning indicates package downgrade, and every non-ABP package has an audit conclusion.

- [ ] **Step 3: Check vulnerabilities**

```powershell
foreach ($project in $projects) {
    dotnet list $project package --vulnerable --include-transitive
}
```

Record advisories and whether remediation belongs in this upgrade or a separate task.

- [ ] **Step 4: Keep the post-upgrade audit uncommitted**

保留依赖图和漏洞审计结果在当前工作区，等待编译与测试完成。

## Task 5: Build and test gates

**Files:**

- Modify only files directly required by a confirmed NuGet compile/runtime error.
- Create/update focused regression tests only when behavior changes.

- [ ] **Step 1: Build backend and templates**

```powershell
dotnet build aspnet-core --no-restore
dotnet build templates/pro-module --no-restore
dotnet build templates/pro-nuget/service --no-restore
```

- [ ] **Step 2: Run automated tests**

```powershell
dotnet test aspnet-core --no-build
dotnet test templates/pro-module --no-build
dotnet test templates/pro-nuget/service --no-build
```

- [ ] **Step 3: Run targeted runtime checks**

Verify JWT login, permissions, multi-tenancy, Mapster mapping, FreeSql queries, CAP publishing/subscription, Hangfire jobs, Nacos configuration, Redis cache, SignalR connection and API response envelopes.

- [ ] **Step 4: Stop on major changes**

Pause for user confirmation if compatibility requires changing `vben28`, public APIs, JWT behavior, database schema, external infrastructure, SDK or TargetFramework.

## Task 6: Release and rollback checklist

**Files:**

- Create: `docs/superpowers/audits/2026-08-09-abp-10-6-release-checklist.md`

- [ ] **Step 1: Verify frozen and excluded paths**

```powershell
git diff -- vben28
git diff -- '**/Migrations/**'
```

Expected: no upgrade-generated changes in either path.

- [ ] **Step 2: Prepare rollback artifacts**

Keep the pre-upgrade container/image, package lock state, configuration snapshot and database backup.

- [ ] **Step 3: Deploy test and then canary production**

Run one complete business cycle in the test environment before production canary. Monitor error rate, authentication, background jobs, messaging, cache and SignalR.

- [ ] **Step 4: Request final approval before commit or push**

完成验收后先汇报全部变更；只有收到明确授权才执行最终 `git commit`，推送操作另行确认。
