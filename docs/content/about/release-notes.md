# 发行说明

## 5.3.2.7

**功能**

* 封装abp 自带模板到BasicManagement

## 5.3.2.5

**功能**

* 移除IdentityServer4 #45

**Bug**

* 导出用户权限错误

## 5.3.2.4

**Bug**

* 升级Vben2.8，组织机构编辑错误 #62

## 5.3.2.3

**功能**

* 权限菜单级联操作 #48

**Bug**

* 统一参数返回值过滤器，空指针异常 #61

## 5.3.2.2

**功能**

* 启用GlobalUsing功能 #56
* 采用Directory.Build.targets管理 nuget包 #55


**Bug**

* Vben 分页组件总条数显示异常 #59

## 5.3.2.1

**功能**

* 调整NotificationManagement聚合设计 #51
* 调整NotificationManagement,Redis配置 #50
* 升级Abp5.3.2


**Bug**

* 多个hangfire定时任务，只执行单个问题 #54
* vue客户端先启动，SignalR不尝试重连 #49
