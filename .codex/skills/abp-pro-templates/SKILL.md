---
name: abp-pro-templates
description: 本项目的模板和脚手架维护规则。修改 templates/pro-module、templates/pro-nuget/service、内部 Lion.AbpPro CLI、生成结构或集中包版本时使用。
---

# ABP Pro 模板和脚手架

## 模板范围

- `templates/pro-module`：可复用业务模块模板。
- `templates/pro-nuget/service`：可复用服务模板。
- `aspnet-core/frameworks/src/Lion.AbpPro.Cli.*`：项目脚手架和替换逻辑。
- `vben28`：除非用户明确要求，不属于模板维护范围。

## 规则

- 模板生成结果必须遵循项目实际规范：ABP 模块、Mapster、EF Core/FreeSql、CAP、全局 using 和测试依赖。
- 修改包版本时，同时检查生产项目和受影响模板中的 `Directory.Build*.targets`。
- 只更新模板实际使用的包，不为了版本整齐升级休眠包。
- 修改文件名、命名空间或项目名之前，检查 CLI 替换键和路径是否仍兼容。
- 模板变更后至少还原或构建一个最小生成项目。

## 升级工作

- ABP 升级不仅是版本替换；集中适配器（例如 CAP）必须完成 API 兼容编译。
- `NU1900` 表示无法访问漏洞审计数据，需要单独排查网络、代理、证书或防火墙。
- 未经用户明确授权，不提交或推送升级分支。
