---
name: abp-pro-data-access
description: 本项目的数据访问规则。修改 EF Core DbContext、实体映射、仓储、FreeSql、数据库提供程序、迁移或持久化测试时使用。
---

# ABP Pro 数据访问

## 提供程序选择

- 遵循受影响模块已有的提供程序：`.EntityFrameworkCore` 或 `.FreeSqlRepository`。
- 不在无关功能中把模块从 EF Core 切换到 FreeSql，或反向切换。
- 应用服务和领域服务只依赖 `IRepository<TEntity, TKey>` 或模块仓储抽象，不直接注入 ORM 上下文。

## EF Core

- 在所属模块通过 `AddAbpDbContext` 注册 DbContext。
- 在现有模型创建扩展中配置实体，并调用 `ConfigureByConvention()`。
- 复用项目已有的表前缀、Schema、长度、索引和删除行为约定。
- 自定义查询放在仓储中，通过 `GetDbSetAsync()` 或 `GetQueryableAsync()` 获取查询源。
- 只读查询在符合附近代码习惯时使用 `AsNoTracking()`。

## FreeSql

- 遵循 `Lion.AbpPro.FreeSqlRepository` 的现有抽象和模块仓储写法。
- 将提供程序特有查询限制在 FreeSql 仓储项目中。

## Migrations

- 未经用户明确要求，不生成、删除、恢复或编辑 Migrations。
- 生成迁移前确认目标 DbContext、启动项目、连接配置以及迁移是否已应用。
- 不在开发任务中直接对生产数据库执行迁移。

## 测试

- 持久化行为放在所属 EF Core 或 FreeSql 测试项目中。
- 只有实际使用 SQLite 的测试项目才添加 `SQLitePCLRaw.lib.e_sqlite3`。
- “没有可用测试”表示测试发现失败或程序集为空，不能当作测试通过。
