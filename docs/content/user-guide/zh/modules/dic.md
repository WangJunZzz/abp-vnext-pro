# 数据字典模块
Abp自带的Setting模块可能满足不了需求，特意提供数据字典模块。
![](../../../img/dic.png)

## 安装

- Lion.Abp.DataDictionaryManagement.Application
- Lion.Abp.DataDictionaryManagement.Application.Contracts
- Lion.Abp.DataDictionaryManagement.Domain
- Lion.Abp.DataDictionaryManagement.Domain.Shared
- Lion.Abp.DataDictionaryManagement.EntityFrameworkCore
- Lion.Abp.DataDictionaryManagement.HttpApi
- Lion.Abp.DataDictionaryManagement.HttpApi.Client

## 模块依赖

- 添加 DependsOn(typeof(DataDictionaryManagementXxxModule)) 特性到对应模块。

- 在EntityFrameworkCore层添加数据库配置在AbpProDbContext.cs的OnModelCreating()方法中添加builder.ConfigureDataDictionaryManagement();

## 实体
**DataDictionary** 表结构：

字段名 | 描述 | 类型
:---|:---|:---
Id |  Id | Guid
TenantId | 租户id | Guid?
Code | 字典编码 | string
DisplayText | 显示名 | string
Description | 描述 | DateTime
Details | 字典明细 | List<DataDictionaryDetail>
IsDeleted | 是否删除 | bool
DeleterId | 删除人 | Guid?
DeletionTime | 删除时间 | DateTime
LastModifierId | 最后修改人 | Guid?
LastModificationTime | 最后修改时间 | DateTime
CreatorId | 创建人 | Guid?
CreationTime | 创建时间 | DateTime

**DataDictionaryDetail** 表结构：

字段名 | 描述 | 类型
:---|:---|:---
Id |  Id | Guid
DataDictionaryId |  所属字典Id | Guid
Order | 排序 | Int
Code | 字典编码 | string
IsEnabled | 启/停用(默认启用) | bool
DisplayText | 显示名 | string
Description | 描述 | DateTime
IsDeleted | 是否删除 | bool
DeleterId | 删除人 | Guid?
DeletionTime | 删除时间 | DateTime
LastModifierId | 最后修改人 | Guid?
LastModificationTime | 最后修改时间 | DateTime
CreatorId | 创建人 | Guid?
CreationTime | 创建时间 | DateTime