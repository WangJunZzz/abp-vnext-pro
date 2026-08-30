---
name: abp-pro-testing
description: 本项目的测试和验证规则。新增或修改领域、应用、EF Core、集成或模块测试，以及运行 dotnet test、解释还原审计构建结果时使用。
---

# ABP Pro 测试和验证

## 测试位置

- 领域规则放在所属 `*.Domain.Tests`。
- 应用用例放在 `*.Application.Tests`，复用现有 ABP 测试基类。
- 持久化行为放在 `*.EntityFrameworkCore.Tests` 或对应 FreeSql 测试项目。
- 复用模块已有测试基类和依赖配置，不重复创建 DI 测试框架。

## 测试原则

- 测试可观察行为：验证、授权、租户隔离、映射、持久化约束和事件效果。
- 使用 `GuidGenerator`、`Clock` 和 ABP 测试服务，避免静态随机状态和环境时间。
- 测试不依赖执行顺序。

## 命令和结果解释

```powershell
dotnet build <受影响项目> --no-restore
dotnet test <测试项目> --no-build
dotnet list <项目> package --vulnerable
```

- 先构建最小受影响项目，再构建共享 Host 或网关。
- 共享输出目录相关的 Host、网关和模块构建必须串行执行。
- `NU1900` 是漏洞数据源不可访问，不是安全审计通过；默认不要关闭审计。
- “没有可用测试”是测试发现失败或程序集为空，不能算测试通过。
- 完成前执行 `git diff --check`，并报告未能执行的命令及原因。
