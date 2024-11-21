# 基础模块

- 把abp自带 账户模块，权限模块，identity模块，setting模块，feature模块，后台任务模块，租户模块封装到BasicManagement

## 安装

- Lion.Abp.BasicManagement.Application
- Lion.Abp.BasicManagement.Application.Contracts
- Lion.Abp.BasicManagement.Domain
- Lion.Abp.BasicManagement.Domain.Shared
- Lion.Abp.BasicManagement.EntityFrameworkCore
- Lion.Abp.BasicManagement.HttpApi
- Lion.Abp.BasicManagement.HttpApi.Client


## 模块依赖

- 添加 DependsOn(typeof(BasicManagementXxxModule)) 特性到对应模块。
- 在EntityFrameworkCore层添加数据库配置在AbpProDbContext.cs的OnModelCreating()方法中添加builder.ConfigureBasicManagement();