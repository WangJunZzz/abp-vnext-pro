# ABP 10.2.0 到 10.6.0 升级设计

## 1. 目标与范围

将后端项目和后端模板中实际参与构建的 ABP 及其相关 NuGet 依赖，调整到与 ABP 10.6.0/`net10.0` 兼容的版本集合。

明确边界：

- `vben28` 目录完全冻结，不读取后写入，不修改前端依赖或代码。
- 第一阶段优先只修改 NuGet 版本和必要的依赖管理文件。
- 暂不主动修改业务代码、公开 API、配置、SDK 或 TargetFramework；EF `Migrations` 内容不纳入本阶段 NuGet 审计。
- 未被实际项目引用的 ABP 模块、数据库 provider 和基础设施包不作为升级对象。
- NuGet 审计覆盖所有直接和传递依赖，不限于 `Volo.Abp.*`；但“审计”不等于所有包都必须升级。
- 项目实际采用 Mapster；不引入 AutoMapper，也不进行 AutoMapper 迁移。
- 根据 `docs/content/user-guide/zh/infrastructure/login.md`，不进行 IdentityServer4/OpenIddict 业务接入改造。

## 2. 现状基线

- 后端和主要模板目标框架为 `net10.0`。
- SDK 由 `aspnet-core/global.json` 约束为 10.0.0，并采用 `latestMajor` roll-forward。
- ABP 版本主要集中在：
  - `aspnet-core/Directory.Build.Volo.targets`
  - `templates/pro-module/Directory.Build.Volo.targets`
  - `templates/pro-nuget/service/Directory.Build.Volo.targets`
- Microsoft/EF Core 版本主要集中在对应的 `Directory.Build.Microsoft.targets`。
- 仓库实际涉及 PostgreSQL、MySQL 测试 provider、Mapster、FreeSql、CAP、Hangfire、Nacos、Redis、SignalR、Serilog 等依赖。

## 3. NuGet 审计与依赖盘点原则

升级前生成实际依赖清单，按以下优先级判定：

1. 项目 `.csproj` 中的直接 `PackageReference`。
2. `dotnet list <project> package --include-transitive` 生成的传递依赖。
3. `Directory.Build.*` 中对已存在 PackageReference 的 `Update` 项。
4. 源码和文档中的功能使用情况，仅用于解释用途，不单独构成升级依据。

依赖分为四类：生产运行时、测试专用、模板专用、未使用/遗留。只审计前三类中实际参与构建的包。每个包记录以下结论之一：

- **必须同步升级**：被 ABP 10.6.0 或 `net10.0` 兼容矩阵约束，或存在已知安全/构建阻断问题。
- **建议升级**：存在兼容的新版本，但不影响本次 ABP 升级，可单独安排。
- **保持当前版本**：没有兼容性、漏洞或构建问题，不为追新版本扩大范围。
- **排除**：未被实际项目解析或仅为历史遗留，不修改。

审计对象至少包括：

- ABP/Volo：`Volo.Abp.*`。
- Microsoft/.NET：`Microsoft.Extensions.*`、ASP.NET Core、EF Core、测试 SDK。
- 数据库和 ORM：Npgsql、Pomelo/MySQL、MongoDB.Driver、SQLite、FreeSql 及 provider。
- 基础设施：CAP 及其 transport/storage、Hangfire、Redis、Nacos、SignalR、Serilog、ElasticSearch、Swashbuckle。
- 测试与质量工具：xUnit、NSubstitute、Shouldly、coverlet、测试日志组件。
- 其他直接 PackageReference 和所有传递依赖，用于发现降级、重复版本和已知漏洞。

中央 targets 中未生效的 `Update` 行可以保留，不视为已升级的功能模块；但必须在审计报告中标记为“未解析/排除”，避免误认为已完成全量升级。

需要特别核对：

- `Volo.Abp.AutoMapper` 目前只出现在一个 EF Core 测试项目中，先确认其是否实际使用；不因该引用扩大业务改造范围。
- OpenIddict 相关引用和迁移表只按测试/历史基线处理，不新增认证配置或业务模块。
- MySQL、MongoDB 等仅在测试或特定模块使用的 provider 单独核验，不默认升级全部 provider。

## 4. 升级方案

### 阶段 A：建立基线

- 创建独立升级分支并确认工作区无未提交的相关修改。
- 记录 `dotnet --info`、当前 NuGet 依赖、全量构建结果和测试结果。
- 备份测试数据库；生产数据库只做备份和迁移演练，不直接操作。
- 明确本次允许修改的路径：`aspnet-core/**`、`templates/**`、本设计文档；禁止触碰 `vben28/**`。

### 阶段 B：只升级 NuGet 版本

- 将实际使用的 `Volo.Abp.*` 包更新到 10.6.0。
- 将与 ABP 10.6.0/`net10.0` 配套的 Microsoft.Extensions、ASP.NET Core、EF Core 包更新到兼容补丁版本，保持同一依赖族一致。
- 对所有实际使用的第三方依赖执行版本、框架兼容性、安全漏洞和传递依赖审计；只有命中“必须同步升级”的包才在本阶段修改版本。
- 对 CAP、FreeSql、Hangfire、Nacos、Redis、Serilog、数据库 provider、Swashbuckle 和测试包分别输出“升级/保持/排除”结论，不做无依据的批量升级。
- 暂不改变 SDK、TargetFramework、编译选项、配置键和 migration 文件。
- 执行 restore 后检查依赖树，确认没有生产项目继续解析到 ABP 10.2.0，也没有出现未经解释的旧版/降级依赖。

建议命令：

```powershell
dotnet restore aspnet-core
dotnet build aspnet-core --no-restore
dotnet test aspnet-core --no-build
dotnet list <project> package --include-transitive
dotnet list <project> package --outdated
dotnet list <project> package --vulnerable --include-transitive
```

模板分别在自身目录执行 restore/build/test，不对 `vben28` 执行命令。审计结果应保存为包清单，包含直接版本、解析版本、来源项目、用途、结论和备注。

### 阶段 C：仅处理由 NuGet 升级直接引起的问题

按错误类型处理，保持最小改动：

- 编译 API 变更：只修改受影响的调用点，并记录前后行为。
- 依赖冲突：优先调整包版本或移除重复直接引用，不做无关重构。
- EF Core provider 编译/运行时不兼容：先锁定兼容版本；只有确认存在模型变化才进入数据库变更评估。
- Mapster 或 `IObjectMapper` 行为异常：先补充回归测试，再决定是否需要代码适配。

任何公开 API、JWT、权限、多租户、消息事件或统一返回值变化都暂停并请求确认。

### 阶段 D：验证与发布

- 运行后端和模板的 build/test 全量验证。
- 验证登录、JWT、权限、多租户、Mapster 映射、FreeSql 查询、CAP、Hangfire、Nacos、Redis、SignalR、日志和网关。
- 对实际使用的 EF Core DbContext 执行 `dotnet ef migrations has-pending-model-changes` 或等效模型差异检查。
- EF `Migrations` 文件可在升级分支中忽略或删除，不作为 NuGet 升级完成条件；只有模型确实稳定且获得确认后，才重新生成新的 migration，并在备份数据库副本演练。
- 测试环境部署并运行一个完整业务周期后再灰度生产；保留旧镜像和旧依赖锁定状态用于回滚。

## 5. 重大变更确认门槛

以下任一情况必须暂停并由项目负责人确认：

- 必须修改 `vben28` 才能兼容后端。
- 必须修改公开 API、DTO、统一返回值或 SignalR 协议。
- JWT 登录、刷新、权限、租户行为发生变化。
- migration 产生真实表、字段、索引、默认值或数据转换变化。
- 必须升级数据库服务器、Redis、RabbitMQ、Nacos 或其他外部基础设施。
- 必须修改 SDK、TargetFramework 或部署镜像。
- 必须把 Mapster 改回 AutoMapper，或改变现有映射策略。

## 6. 验收标准

- 后端实际项目和两个后端模板 restore/build/test 通过。
- 生产项目依赖图中不存在未解释的 ABP 10.2.0 残留。
- 生产、测试和模板依赖图中的非 ABP 包均有“必须同步升级/建议升级/保持/排除”结论。
- 未使用的 ABP 模块和 provider 没有被主动升级或新增引用。
- `git diff -- vben28` 为空。
- 数据库没有未经确认的结构变化；现有或删除的 EF `Migrations` 文件不作为本阶段验收项。
- 关键业务和基础设施回归通过，且具备旧版本部署物和数据库备份回滚路径。
