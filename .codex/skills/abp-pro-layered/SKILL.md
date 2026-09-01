---
name: abp-pro-layered
description: 本项目的分层 DDD 开发规则。新增或修改实体、领域服务、DTO、应用服务、控制器、权限或业务模块时使用。
---

# ABP Pro 分层开发

## 代码归属

- `*.Domain.Shared`：常量、枚举、本地化、权限和共享契约。
- `*.Domain`：聚合、值对象、领域服务、仓储接口和领域规则。
- `*.Application.Contracts`：DTO 和应用服务接口。
- `*.Application`：用例编排、授权和 Mapster 映射。
- `*.EntityFrameworkCore` 或 `*.FreeSqlRepository`：持久化实现。
- `*.HttpApi`：控制器和 HTTP 层代码。

模块代码放在所属模块中，不要把模块业务规则放入主服务。

## 新功能流程

1. 在聚合或领域服务中定义业务不变量和状态变更。
2. 在 `Domain.Shared` 中添加常量、枚举、本地化和权限名称。
3. 只有通用仓储无法清晰表达查询时，才增加自定义仓储方法。
4. 在 `Application.Contracts` 定义输入、输出 DTO 和服务契约。
5. 在 `Application` 实现用例、授权和 Mapster 映射。
6. 只有需要明确 HTTP 接口时才增加控制器。
7. 增加针对领域规则或应用用例的测试。

## 禁止事项

- 不在控制器或仓储中编写业务规则。
- 不直接把实体作为 API DTO 暴露。
- 不绕过聚合方法直接修改聚合内部状态。
- 不在领域层或应用层依赖具体数据库实现。

## 模板

可复用模块以 `templates/pro-module` 为准；标准服务模板以 `templates/pro-nuget/service` 为准。修改模板时必须同步现有项目的分层和包版本约定。
